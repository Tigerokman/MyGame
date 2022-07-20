using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : Bar
{
    [SerializeField] private AllSpawners _spawners;

    private void OnEnable()
    {
        _spawners.EnemyCountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawners.EnemyCountChanged -= OnValueChanged;
    }
}
