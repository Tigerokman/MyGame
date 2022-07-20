using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _startPrice;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _upgradeValue;
    [SerializeField] private int _buyPriceDecrease;

    protected Player _player;
    private int _currentPrice;

    public string Label => _label;
    public int Price => _currentPrice;
    public Sprite Icon => _icon;
    public int UpgradeValue => _upgradeValue;

    public void Init(Player player)
    {
        _currentPrice = _startPrice;
        _player = player;
    }

    public void PriceDecrease()
    {
        _currentPrice += _buyPriceDecrease;
    }
}

