using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ExpirienceAdded += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ExpirienceAdded -= OnValueChanged;
    }
}
