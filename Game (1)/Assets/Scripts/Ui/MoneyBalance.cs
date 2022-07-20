using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.MoneyChanged += MoneyViewChange;
        _money.text = _player.Money.ToString();
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= MoneyViewChange;
    }

    private void MoneyViewChange()
    {
        _money.text = _player.Money.ToString();
    }
}
