using UnityEngine;

namespace Assets.Scripts.Input
{
  public class InputHandler : MonoBehaviour
  {
    public AI_Player AI_Player { get; private set; }

    public IInput IInput { get; private set; }

    private void Awake()
    {
      AI_Player = new AI_Player();
    }

    private void OnEnable()
    {
      AI_Player.Enable();

      InitializeInput();
    }

    private void OnDisable()
    {
      AI_Player.Disable();
    }

    private void InitializeInput()
    {
      if (SystemInfo.deviceType == DeviceType.Handheld)
        IInput = new MobileInput(this);
      else
        IInput = new DesktopInput(this);
    }
  }
}