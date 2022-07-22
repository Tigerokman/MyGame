using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private UpgradeView _template;
    [SerializeField] private GameObject _upgradeContainer;
    [SerializeField] private GameObject _notMoneyText;

    private void Start()
    {
        for (int i = 0; i < _upgrades.Count; i++)
        {
            _upgrades[i].Init();
            AddUpgrade(_upgrades[i]);
        }
    }

    private void OnEnable()
    {
        _notMoneyText.SetActive(false);
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
        bool canPay = _wallet.CanPay(upgrade.Price);

        if (canPay)
        {
            _notMoneyText.SetActive(false);
            int upgradeCount = _playerStats.Upgrade(upgrade);
            upgrade.PriceDecrease();
            view.PriceChange(upgrade.Price);

            if (upgradeCount == 0)
                view.UpgradeButtonClick -= OnSellUpgradeButtonClick;
        }
        else if (canPay == false)
        {
            _notMoneyText.SetActive(true);
        }
    }
}
