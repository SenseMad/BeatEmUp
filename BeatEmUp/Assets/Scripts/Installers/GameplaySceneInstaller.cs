using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
  public class GameplaySceneInstaller : MonoInstaller
  {
    [SerializeField] private Enemy _enemyPrefab;

    public override void InstallBindings()
    {
      Container.Bind<Character>().FromComponentInHierarchy().AsSingle().NonLazy();

      Container.BindFactory<Enemy, EnemyFactory>().FromComponentInNewPrefab(_enemyPrefab).AsTransient();
    }
  }
}