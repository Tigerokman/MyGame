using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _upgradeButton;

    private Upgrade _upgrade;

    public event UnityAction<Upgrade,UpgradeView> UpgradeButtonClick;

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render (Upgrade upgrade)
    {
        _upgrade = upgrade;

        _label.text = upgrade.Label;
        _price.text = upgrade.Price.ToString();
        _icon.sprite = upgrade.Icon;
    }

    public void PriceChange(int price)
    {
        _price.text = price.ToString();
    }    

    private void OnButtonClick()
    {
        UpgradeButtonClick?.Invoke(_upgrade,this);
    }
}
