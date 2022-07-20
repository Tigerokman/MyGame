using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevelUpChoise : MonoBehaviour
{
    [SerializeField] private List<LevelUp> _levelUps;
    [SerializeField] private PlayerStats _player;
    [SerializeField] private LevelUpView _template;
    [SerializeField] private GameObject _levelUpContainer;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        for (int i = 0; i < _levelUps.Count; i++)
        {
            _levelUps[i].Init(_player);
            AddUpgrade(_levelUps[i]);
        }
    }

    private void AddUpgrade(LevelUp levelUp)
    {
        var view = Instantiate(_template, _levelUpContainer.transform);
        view.UpgradeButtonClick += LevelUpUpgradeButtonClick;
        view.Render(levelUp);
    }

    private void LevelUpUpgradeButtonClick(LevelUp levelUp, LevelUpView view)
    {
        int maxStat = 999;
        int stat = _player.LevelUp(levelUp.Label);

        if (stat == maxStat)
        {
            view.UpgradeButtonClick -= LevelUpUpgradeButtonClick;
        }
        _panel.SetActive(false);
        Time.timeScale = 1;
    }
}
