using UnityEngine;

public class Enemy : MonoBehaviour
{
  [Header("Move")]
  [SerializeField, Min(0)] private float _speed;
  [SerializeField, Min(0)] private float _rotationSpeed;

  [Header("Attack")]
  [SerializeField, Min(0)] private int _damage;
  [SerializeField, Min(0)] private float _rangeVisibility;
  [SerializeField, Min(0)] private float _attackRange;
  [SerializeField, Min(0)] private float _attackDelay;

  [Header("Health")]
  [SerializeField] private Health _health;

  [Header("Collider")]
  [SerializeField] private Collider _attackCollider;
  [SerializeField] private LayerMask _layerMask;

  public float Speed => _speed;
  public float RotationSpeed => _rotationSpeed;

  public int Damage => _damage;
  public float RangeVisibility => _rangeVisibility;
  public float AttackRange => _attackRange;
  public float AttackDelay => _attackDelay;

  public Health Health => _health;

  public Collider AttackCollider => _attackCollider;
  public LayerMask LayerMask => _layerMask;

  public bool IsAttack { get; private set; }

  private void Start()
  {
    _health.Initialize();
  }

  private void OnEnable()
  {
    _health.OnInstantlyKill += OnInstantlyKill;
  }

  private void OnDisable()
  {
    _health.OnInstantlyKill -= OnInstantlyKill;
  }

  private void OnInstantlyKill()
  {
    Destroy(gameObject);
  }

  public void Attack(bool parValue)
  {
    IsAttack = parValue;
  }
}