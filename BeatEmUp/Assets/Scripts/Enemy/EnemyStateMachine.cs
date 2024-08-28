using UnityEngine;
using Zenject;

public class EnemyStateMachine : MonoBehaviour
{
  private EnemyBaseState currentState;

  public EnemyIdleState IdleState { get; private set; }
  public EnemyFollowState FollowState { get; private set; }
  public EnemyAttackState AttackState { get; private set; }

  public Character Character { get; private set; }
  public Vector3 CharacterPosition => Character.transform.position;
  public bool CharacterGameobject => Character.gameObject.activeSelf;

  public Enemy Enemy { get; private set; }

  public Animator Animator { get; private set; }

  [Inject]
  private void Construct(Character parCharacter)
  {
    Character = parCharacter;
  }

  private void Awake()
  {
    IdleState = new EnemyIdleState();
    FollowState = new EnemyFollowState();
    AttackState = new EnemyAttackState();

    Animator = GetComponent<Animator>();

    Enemy = GetComponent<Enemy>();
  }

  public void Start()
  {
    currentState = IdleState;

    currentState.EnterState(this);
  }

  private void Update()
  {
    currentState.UpdateState(this);
  }

  public void SwithState(EnemyBaseState parState)
  {
    currentState = parState;
    parState.EnterState(this);
  }
}