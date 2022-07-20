using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] Image _charge;
    [SerializeField] Image _fireWay;

    private void OnEnable()
    {
        _playerAttack.ColldownChange += EnableChange;
        _playerMovement.ColldownChange += EnableChange;
    }

    private void OnDisable()
    {
        _playerAttack.ColldownChange -= EnableChange;
        _playerMovement.ColldownChange -= EnableChange;
    }

    private void EnableChange(string name)
    {
        string skill = "Skill";
        string charge = "Charge";

        if (skill == name)
            _fireWay.enabled = !_fireWay.enabled;
        else if (charge == name)
            _charge.enabled = !_charge.enabled;
    }    
}
