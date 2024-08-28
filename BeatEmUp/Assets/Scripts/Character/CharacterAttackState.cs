using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterBaseState
{
  private float currentDelayAttack = 0f;

  private HashSet<Collider> hitEnemies = new HashSet<Collider>();

  public override void EnterState(CharacterStateMachine parState)
  {
    parState.Animator.SetTrigger("IsAttack");
  }

  public override void UpdateState(CharacterStateMachine parState)
  {
    Vector3 point1 = parState.Character.AttackCollider.bounds.center + Vector3.up * parState.Character.AttackCollider.bounds.extents.y;
    Vector3 point2 = parState.Character.AttackCollider.bounds.center - Vector3.up * parState.Character.AttackCollider.bounds.extents.y;
    float radius = parState.Character.AttackCollider.bounds.extents.x;

    Collider[] hitColliders = Physics.OverlapCapsule(point1, point2, radius);

    parState.Character.Attack(true);

    foreach (Collider collider in hitColliders)
    {
      if (hitEnemies.Add(collider))
      {
        if (collider.TryGetComponent(out Enemy parEnemy))
        {
          parEnemy.Health.TakeHealth(parState.Character.Damage);
        }
      }
    }

    currentDelayAttack += Time.deltaTime;
    if (currentDelayAttack >= 1)
    {
      currentDelayAttack = 0;

      parState.Character.Attack(false);
      parState.SwithState(parState.IdleState);

      hitEnemies.Clear();
    }
  }
}