using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.MoneyChanged += MoneyViewChange;
        _money.text = _wallet.Money.ToString();
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= MoneyViewChange;
    }

    private void MoneyViewChange()
    {
        _money.text = _wallet.Money.ToString();
    }
}
