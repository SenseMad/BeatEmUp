using UnityEngine;

public class CharacterIdleState : CharacterBaseState
{
  public override void EnterState(CharacterStateMachine parState)
  {
    parState.Animator.SetFloat("Speed", 0);
  }

  public override void UpdateState(CharacterStateMachine parState)
  {
    Vector2 input = parState.InputHandler.IInput.Move();

    if (input.x != 0 || input.y != 0)
    {
      parState.SwithState(parState.WalkState);
    }
  }
}