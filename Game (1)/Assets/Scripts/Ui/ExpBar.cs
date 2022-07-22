using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : Bar
{
    [SerializeField] private PlayerLevel _playerLevel;

    private void OnEnable()
    {
        _playerLevel.ExpirienceAdded += OnValueChanged;
    }

    private void OnDisable()
    {
        _playerLevel.ExpirienceAdded -= OnValueChanged;
    }
}
