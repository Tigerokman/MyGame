using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{

    public int Money { get; private set; } = 0;

    public event UnityAction MoneyChanged;

    public bool CanPay(int price)
    {
        bool canPay = false;

        if (Money >= price)
        {
            canPay = true;
            Pay(price);
        }

        return canPay;
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
