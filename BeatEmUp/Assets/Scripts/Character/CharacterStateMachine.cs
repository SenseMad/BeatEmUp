using UnityEngine;
using Zenject;

using Assets.Scripts.Input;

public class CharacterStateMachine : MonoBehaviour
{
  private CharacterBaseState currentState;

  public CharacterIdleState IdleState { get; private set; }
  public CharacterWalkState WalkState { get; private set; }
  public CharacterAttackState AttackState { get; private set; }
  public CharacterDiedState DiedState { get; private set; }

  public Character Character { get; private set; }

  public Animator Animator { get; private set; }

  public InputHandler InputHandler { get; private set; }

  [Inject]
  private void Construct(InputHandler parInputHandler, Character parCharacter)
  {
    InputHandler = parInputHandler;
    Character = parCharacter;
  }

  private void Awake()
  {
    IdleState = new CharacterIdleState();
    WalkState = new CharacterWalkState();
    AttackState = new CharacterAttackState();
    DiedState = new CharacterDiedState();

    Animator = GetComponent<Animator>();
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

  public void SwithState(CharacterBaseState parState)
  {
    currentState = parState;
    parState.EnterState(this);
  }
}