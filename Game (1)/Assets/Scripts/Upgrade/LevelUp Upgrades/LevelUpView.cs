using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUpView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _upgradeButton;

    private LevelUp _levelUp;

    public event UnityAction<LevelUp, LevelUpView> UpgradeButtonClick;

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(LevelUp levelUp)
    {
        _levelUp = levelUp;

        _label.text = levelUp.Label;
        _icon.sprite = levelUp.Icon;
    }

    private void OnButtonClick()
    {
        UpgradeButtonClick?.Invoke(_levelUp, this);
    }
}
