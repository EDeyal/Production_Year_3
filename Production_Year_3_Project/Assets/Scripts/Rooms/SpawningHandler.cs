using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawningHandler: ICheckValidation
{
    [SerializeField] List<BaseEnemy> _enemies;

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

    public void CheckValidation()
    {
        if (_enemies.Count == 0)
        {
            throw new System.Exception("Spawning Handler has no enemies to spawn");
        }
    }
}
