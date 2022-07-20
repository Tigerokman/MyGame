using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private UpgradeView _template;
    [SerializeField] private GameObject _upgradeContainer;

    private void Start()
    {
        for (int i = 0; i < _upgrades.Count; i++)
        {
            _upgrades[i].Init(_player);
            AddUpgrade(_upgrades[i]);
        }
    }

    private void AddUpgrade(Upgrade upgrade)
    {
        var view = Instantiate(_template, _upgradeContainer.transform);
        view.UpgradeButtonClick += OnSellUpgradeButtonClick;
        view.Render(upgrade);
    }

    private void OnSellUpgradeButtonClick(Upgrade upgrade, UpgradeView view)
    {
        TrySellUpgrade(upgrade, view);
    }

    private void TrySellUpgrade(Upgrade upgrade, UpgradeView view)
    {
        if (_player.Money >= upgrade.Price)
        {
            int upgradeCount = _playerStats.Upgrade(upgrade.Label);
            upgrade.PriceDecrease();
            view.PriceChange(upgrade.Price);

            if (upgradeCount == 0)
                view.UpgradeButtonClick -= OnSellUpgradeButtonClick;
        }
    }
}
