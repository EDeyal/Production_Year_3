using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawningHandler
{
    [SerializeField] List<BaseEnemy> _enemies;
    public List<BaseEnemy> Enemies => _enemies;

    public void SpawnEnemies()
    {
        Debug.Log("Spawning enemies");
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    public void DespawnEnemies()
    {
        Debug.Log("Despawning enemies");
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }
}
