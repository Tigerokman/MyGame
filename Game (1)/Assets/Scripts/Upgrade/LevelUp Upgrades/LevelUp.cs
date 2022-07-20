using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;

    protected PlayerStats _player;

    public string Label => _label;
    public Sprite Icon => _icon;

    public void Init(PlayerStats player)
    {
        _player = player;
    }
}
