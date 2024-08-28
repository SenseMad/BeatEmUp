using Zenject;

using Assets.Scripts.Input;

namespace Assets.Scripts.Installers
{
  public class GlobalInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<InputHandler>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
  }
}