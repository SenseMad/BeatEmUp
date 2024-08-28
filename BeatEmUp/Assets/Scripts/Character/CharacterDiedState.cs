using System.Collections;
using UnityEngine;

public class CharacterDiedState : CharacterBaseState
{
  public override void EnterState(CharacterStateMachine parState)
  {
    parState.Animator.SetTrigger("IsDied");
  }

  public override void UpdateState(CharacterStateMachine parState)
  {

  }
}