using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Player))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private ArmorUpgrade _armorUpgrade;
    [SerializeField] private WeaponUpgrade _weaponUpgrade;
    [SerializeField] private LevelUpAgility _levelUpAgility;
    [SerializeField] private LevelUpKnowledge _levelUpKnowledge;
    [SerializeField] private LevelUpStrength _levelUpStrength;

    private PlayerMovement _playerMovement;
    private Player _player;
    private int _countArmorUpgrade = 50;
    private int _countWeaponUpgrade = 10;
    private float _attackRangeUp = 0.02f;


    public int Regeneration { get; private set; } = 1;
    public int Damage { get; private set; } = 1;
    public float AttackRange { get; private set; } = 1;
    public int Agility { get; private set; } = 0;
    public int Knowledge { get; private set; } = 0;
    public int Strength { get; private set; } = 0;
    public int UpgradeArmor { get; private set; } = 0;
    public int UpgradeWeapon { get; private set; } = 0;


    public string LevelUpAgilityName => _levelUpAgility.Label;
    public string LevelUpKnowledgeName => _levelUpKnowledge.Label;
    public string LevelUpStrengthName => _levelUpStrength.Label;
    public int MaxHealth => _maxHealth;

    public event UnityAction StatsChanged;
    public event UnityAction MaxHealthIncreased;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
        _player.Dying += Death;
    }

    private void OnDisable()
    {
        _player.Dying -= Death;
    }

    public int LevelUp(string nameLevelUp)
    {
        int template = 0;

        if (_levelUpAgility.Label == nameLevelUp)
        {
            Agility++;
            template = Agility;
            _playerMovement.AgilityUp();
            AgilityUp();
        }
        else if (_levelUpKnowledge.Label == nameLevelUp)
        {
            Knowledge++;
            template = Knowledge;
        }
        else if (_levelUpStrength.Label == nameLevelUp)
        {
            Strength++;
            template = Strength;
            StrengthUp();
        }

        StatsChanged?.Invoke();
        return template;
    }

    public int Upgrade(Upgrade upgrade)
    {
        int template = 0;

        if (_armorUpgrade == upgrade)
        {
            template = ArmorUpgrade();
        }
        else if (_weaponUpgrade == upgrade)
        {
            template = DamageUpgrade();
        }

        return template;
    }


    private void AgilityUp()
    {
        AttackRange += _attackRangeUp;
    }

    private void StrengthUp()
    {
        Regeneration++;
    }

    private void Death()
    {
        Regeneration = 0;
    }

    private int DamageUpgrade()
    {
        int template = 0;

        Damage += _weaponUpgrade.UpgradeValue;
        UpgradeWeapon++;
        _countWeaponUpgrade--;
        template = _countWeaponUpgrade;

        return template;
    }

    private int ArmorUpgrade()
    {
        int template = 0;

        _maxHealth += _armorUpgrade.UpgradeValue;
        MaxHealthIncreased?.Invoke();
        UpgradeArmor++;
        _countArmorUpgrade--;
        template = _countWeaponUpgrade;

        return template;
    }
}
