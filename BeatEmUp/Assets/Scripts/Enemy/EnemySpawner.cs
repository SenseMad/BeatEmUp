using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Enemy _enemyPrefab;
  [SerializeField] private int _maxEnemies;
  [SerializeField] private int _spawnInterval;
  [SerializeField] private float _gameTimeMultiplier = 1.0f;

  [SerializeField] private List<Transform> _spawnPoints;

  private List<Enemy> activeEnemies = new List<Enemy>();
  private float elapsedTime = 0f;

  private EnemyFactory enemyFactory;

  [Inject]
  private void Construct(EnemyFactory parEnemyFactory)
  {
    enemyFactory = parEnemyFactory;
  }

  private void Update()
  {
    elapsedTime += Time.deltaTime;
    if (activeEnemies.Count < _maxEnemies && elapsedTime >= _spawnInterval)
    {
      SpawnEnemy();
      elapsedTime = 0f;
    }
  }

  private void SpawnEnemy()
  {
    Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

    Enemy newEnemy = enemyFactory.Create();
    newEnemy.transform.position = spawnPoint.position;

    activeEnemies.Add(newEnemy);
  }

  public void OnEnemyDestroyed(Enemy enemy)
  {
    activeEnemies.Remove(enemy);
  }
}