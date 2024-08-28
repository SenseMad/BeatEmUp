using System.Collections;
using UnityEngine;

public class EnemyFollowState : EnemyBaseState
{
  public override void EnterState(EnemyStateMachine parState) { }

  public override void UpdateState(EnemyStateMachine parState)
  {
    if (!parState.CharacterGameobject)
      return;

    Vector3 direction = (parState.CharacterPosition - parState.transform.position).normalized;

    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    parState.transform.rotation = Quaternion.Slerp(parState.transform.rotation, lookRotation, Time.deltaTime * parState.Enemy.RotationSpeed);

    Vector3 targetPosition = new Vector3(parState.CharacterPosition.x, parState.transform.position.y, parState.CharacterPosition.z);
    Vector3 velocity = Vector3.MoveTowards(parState.transform.position, targetPosition, parState.Enemy.Speed * Time.deltaTime);
    parState.transform.position = velocity;

    parState.Animator.SetFloat("Speed", velocity.z);

    if (Vector3.Distance(parState.transform.position, parState.CharacterPosition) < parState.Enemy.AttackRange)
    {
      parState.SwithState(parState.AttackState);
    }

    if (Vector3.Distance(parState.transform.position, parState.CharacterPosition) > parState.Enemy.RangeVisibility)
    {
      parState.SwithState(parState.IdleState);
    }
  }
}