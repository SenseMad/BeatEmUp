using UnityEngine;

namespace Assets.Scripts.Input
{
  public class MobileInput : IInput
  {
    private InputHandler inputHandler;

    public MobileInput(InputHandler parInputHandler)
    {
      inputHandler = parInputHandler;
    }

    public Vector2 Move()
    {
      return inputHandler.AI_Player.Player.Move.ReadValue<Vector2>();
    }
  }
}