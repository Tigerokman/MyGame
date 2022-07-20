using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _prevoisWaveCount = 0;

    public event UnityAction AllEnemySpawned;
    public event UnityAction EnemyCountChanged;
    public event UnityAction<int, int> ResetEnemyCount;

    private void Start()
    {
        SetWave(_currentWaveNumber);
        ResetEnemyCount?.Invoke(_currentWave.Count, _prevoisWaveCount);
        _prevoisWaveCount = _currentWave.Count;
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            EnemyCountChanged?.Invoke();
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }


    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
        ResetEnemyCount?.Invoke(_currentWave.Count,_prevoisWaveCount);
        _prevoisWaveCount = _currentWave.Count;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _player.AddReward(enemy.Reward, enemy.Expirience);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
