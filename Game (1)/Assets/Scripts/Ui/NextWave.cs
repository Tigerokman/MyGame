using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private Button _nextWaveButton;

    private PlayerInput _playerInput;
    private int _spawnersEnd;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.NextWave.performed += ctx => OnNextWave();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _nextWaveButton.onClick.AddListener(OnNextWave);

        foreach (var spawner in _spawners)
            spawner.AllEnemySpawned += OnAllEnemySpawned;    
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _nextWaveButton.onClick.RemoveListener(OnNextWave);

        foreach (var spawner in _spawners)
            spawner.AllEnemySpawned -= OnAllEnemySpawned;
    }

    public void OnAllEnemySpawned()
    {
        _spawnersEnd++;

        if (_spawnersEnd == _spawners.Count)
            _nextWaveButton.gameObject.SetActive(true);
    }

    public void OnNextWave()
    {

        if (_spawnersEnd == _spawners.Count)
        {
            Debug.Log("G");

            foreach (var spawner in _spawners)
                spawner.NextWave();

            _nextWaveButton.gameObject.SetActive(false);
            _spawnersEnd = 0;
        }
    }
}