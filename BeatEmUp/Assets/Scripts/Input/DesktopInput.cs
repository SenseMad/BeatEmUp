using UnityEngine;

namespace Assets.Scripts.Input
{
  public class DesktopInput : IInput
  {
    private InputHandler inputHandler;

    public DesktopInput(InputHandler parInputHandler)
    {
      inputHandler = parInputHandler;
    }

    public Vector2 Move()
    {
      return inputHandler.AI_Player.Player.Move.ReadValue<Vector2>();
    }
  }
}