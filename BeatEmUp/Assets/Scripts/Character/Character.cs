using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

using Assets.Scripts.Input;

public class Character : MonoBehaviour
{
  [SerializeField] private Collider _attackCollider;

  [SerializeField] private Health _health;

  [SerializeField] private int _damage;

  private float currentDelayDied = 0f;
  private bool isDied;
  
  private CharacterStateMachine stateMachine;

  public Collider AttackCollider => _attackCollider;

  public Health Health => _health;

  public int Damage => _damage;

  public bool IsAttack { get; private set; }

  public InputHandler InputHandler { get; private set; }

  [Inject]
  private void Construct(InputHandler parInputHandler)
  {
    InputHandler = parInputHandler;
  }

  private void Awake()
  {
    stateMachine = GetComponent<CharacterStateMachine>();
  }

  private void Start()
  {
    _health.Initialize();
  }

  private void OnEnable()
  {
    InputHandler.AI_Player.Player.Attack.performed += Attack_performed;
    _health.OnInstantlyKill += OnInstantlyKill;
  }

  private void OnDisable()
  {
    InputHandler.AI_Player.Player.Attack.performed -= Attack_performed;

    _health.OnInstantlyKill -= OnInstantlyKill;
  }

  private void Update()
  {
    if (isDied)
    {
      currentDelayDied += Time.deltaTime;
      if (currentDelayDied >= 0.7f)
      {
        StartCoroutine(SceneRestart());
      }
    }
  }

  public void Attack(bool parValue)
  {
    IsAttack = parValue;
  }

  private void Attack_performed(InputAction.CallbackContext obj)
  {
    if (IsAttack)
      return;

    stateMachine.SwithState(stateMachine.AttackState);
    IsAttack = true;
  }

  private void OnInstantlyKill()
  {
    isDied = true;
    stateMachine.SwithState(stateMachine.DiedState);
  }

  private IEnumerator SceneRestart()
  {
    yield return new WaitForSeconds(1.0f);

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}