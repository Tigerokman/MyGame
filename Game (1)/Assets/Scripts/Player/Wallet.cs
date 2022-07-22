using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerStats))]
public class Wallet : MonoBehaviour
{
    private PlayerStats _playerStats;

    public int Money { get; private set; } = 0;

    public event UnityAction MoneyChanged;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _playerStats.UpgradeByed += Pay;
    }

    private void OnDisable()
    {
        _playerStats.UpgradeByed -= Pay;
    }

    public void AddReward(int money)
    {
        Money += money;
        MoneyChanged?.Invoke();
    }

        private void Pay(int price)
    {
        Money -= price;
        MoneyChanged?.Invoke();
    }
}
