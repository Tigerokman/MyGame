using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllSpawners : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;

    private int _allEnemy;
    private int _currentCountEnemy;

    public event UnityAction<int, int> EnemyCountChanged;

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.ResetEnemyCount += ResetEnemyCount;
            spawner.EnemyCountChanged += EnemyChanged;
        }
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.ResetEnemyCount -= ResetEnemyCount;
            spawner.EnemyCountChanged -= EnemyChanged;
        }
    }

    public void EnemyChanged()
    {
        _currentCountEnemy++;
        EnemyCountChanged?.Invoke(_currentCountEnemy, _allEnemy);
    }

    public void ResetEnemyCount(int maxCount, int previosWaveCount)
    {
        _allEnemy -= previosWaveCount;
        _allEnemy += maxCount;
        _currentCountEnemy = 0;
        EnemyCountChanged?.Invoke(_currentCountEnemy, _allEnemy);
    }
}
