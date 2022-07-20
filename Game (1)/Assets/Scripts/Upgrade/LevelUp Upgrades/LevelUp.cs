using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;

    protected Player _player;

    public string Label => _label;
    public Sprite Icon => _icon;

    public void Init(Player player)
    {
        _player = player;
    }
}
