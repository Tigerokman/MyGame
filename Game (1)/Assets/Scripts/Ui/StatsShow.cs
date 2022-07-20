using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsShow : MonoBehaviour
{
    [SerializeField] private TMP_Text _stats;
    [SerializeField] private PlayerStats _player;

    private void OnEnable()
    {
        _player.StatsChanged += StatsShowChanged;
        StatsShowChanged();
    }

    private void OnDisable()
    {
        _player.StatsChanged -= StatsShowChanged;
    }

    private void StatsShowChanged()
    {
        _stats.text = _player.LevelUpStrengthName + " - " + _player.Strength + "     " + _player.LevelUpAgilityName + " - " + _player.Agility + "     " + _player.LevelUpKnowledgeName + " - " + _player.Knowledge;
    }
}
