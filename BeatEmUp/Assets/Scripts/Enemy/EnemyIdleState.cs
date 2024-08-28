using UnityEngine;
using Zenject;

public class EnemyIdleState : EnemyBaseState
{
  public override void EnterState(EnemyStateMachine parState)
  {
    parState.Animator.SetFloat("Speed", 0);
  }

  public override void UpdateState(EnemyStateMachine parState)
  {
    if (!parState.CharacterGameobject)
      return;

    if (Vector3.Distance(parState.transform.position, parState.CharacterPosition) < parState.Enemy.RangeVisibility)
    {
      parState.SwithState(parState.FollowState);
    }
  }
}