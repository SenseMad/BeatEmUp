using UnityEngine;

public class CharacterWalkState : CharacterBaseState
{
  private CharacterMovement movement;
  private float animationBlend;
  private Vector3 velocity;

  public override void EnterState(CharacterStateMachine parState)
  {
    if (movement == null)
      movement = parState.GetComponent<CharacterMovement>();
  }

  public override void UpdateState(CharacterStateMachine parState)
  {
    Vector2 frameInput = Vector3.ClampMagnitude(parState.InputHandler.IInput.Move(), 1.0f);
    Vector3 moveDirection = new Vector3(-frameInput.y, 0.0f, frameInput.x);
    moveDirection *= movement.Speed;

    if (moveDirection != Vector3.zero)
      animationBlend = Mathf.Lerp(animationBlend, movement.Speed, movement.Acceleration * Time.deltaTime);
    else
      animationBlend = Mathf.Lerp(animationBlend, 0f, movement.Deceleration * Time.deltaTime);

    velocity = Vector3.Lerp(velocity, new Vector3(moveDirection.x, velocity.y, moveDirection.z), Time.deltaTime * (moveDirection.sqrMagnitude > 0.0f ? movement.Acceleration : movement.Deceleration));
    Vector3 applied = velocity * Time.deltaTime;
    movement.CharacterController.Move(applied);

    parState.Animator.SetFloat("Speed", velocity.z);

    if (animationBlend <= 0.01f && (velocity.x <= 0.01f || velocity.y <= 0.01f))
      parState.SwithState(parState.IdleState);
  }
}