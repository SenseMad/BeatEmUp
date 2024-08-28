using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
  private float currentDelayAttack = 0f;

  public override void EnterState(EnemyStateMachine parState)
  {
    currentDelayAttack = 0;

    parState.Enemy.Attack(false);
  }

  public override void UpdateState(EnemyStateMachine parState)
  {
    if (!parState.CharacterGameobject)
      return;

    Vector3 direction = (parState.CharacterPosition - parState.transform.position).normalized;

    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    parState.transform.rotation = Quaternion.Slerp(parState.transform.rotation, lookRotation, Time.deltaTime * parState.Enemy.RotationSpeed);

    if (Vector3.Distance(parState.transform.position, parState.CharacterPosition) > parState.Enemy.AttackRange)
    {
      parState.SwithState(parState.FollowState);
      return;
    }

    currentDelayAttack += Time.deltaTime;
    if (currentDelayAttack >= 1)
    {
      currentDelayAttack = 0;

      parState.Enemy.Attack(false);
    }

    if (!parState.Enemy.IsAttack)
    {
      Vector3 point1 = parState.Enemy.AttackCollider.bounds.center + Vector3.up * parState.Enemy.AttackCollider.bounds.extents.y;
      Vector3 point2 = parState.Enemy.AttackCollider.bounds.center - Vector3.up * parState.Enemy.AttackCollider.bounds.extents.y;
      float radius = parState.Enemy.AttackCollider.bounds.extents.x;

      Collider[] hitColliders = Physics.OverlapCapsule(point1, point2, radius + 0.3f, parState.Enemy.LayerMask);

      parState.Enemy.Attack(true);
      parState.Animator.SetTrigger("IsAttack");

      foreach (Collider collider in hitColliders)
      {
        if (collider.TryGetComponent(out Character parCharacter))
        {
          parCharacter.Health.TakeHealth(parState.Enemy.Damage);
          break;
        }
      }
    }
  }
}