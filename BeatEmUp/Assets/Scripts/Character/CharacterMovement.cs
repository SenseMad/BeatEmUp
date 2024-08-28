using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
  [SerializeField, Min(0)] private float _speed = 10.0f;
  [SerializeField, Min(0)] private float _acceleration = 2.0f;
  [SerializeField, Min(0)] private float _deceleration = 2.0f;

  public float Speed => _speed;
  public float Acceleration => _acceleration;
  public float Deceleration => _deceleration;

  public CharacterController CharacterController { get; private set; }

  private void Awake()
  {
    CharacterController = GetComponent<CharacterController>();
  }
}