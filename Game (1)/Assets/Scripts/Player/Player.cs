using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private ArmorUpgrade _armorUpgrade;
    [SerializeField] private WeaponUpgrade _weaponUpgrade;
    [SerializeField] private LevelUpAgility _levelUpAgility;
    [SerializeField] private LevelUpKnowledge _levelUpKnowledge;
    [SerializeField] private LevelUpStrength _levelUpStrength;

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private bool _isInvulnerability = false;
    private int _currentHealth;
    private int _regeneration = 1;
    private int _cooldownRegeneration = 3;
    private int _expirienceToLevelUp = 100;
    private int _damage = 1;
    private float _attackRange = 1;
    private float _currentCooldownRegeneration = 0;
    private int _countArmorUpgrade = 50;
    private int _countWeaponUpgrade = 10;

    public int Agility { get; private set; } = 0;
    public int Knowledge { get; private set; } = 0;
    public int Strength { get; private set; } = 0;
    public int Money { get; private set; } = 0;
    public int Experience { get; private set; } = 0;
    public int Level { get; private set; } = 1;
    public int UpgradeArmor { get; private set; } = 0;
    public int UpgradeWeapon { get; private set; } = 0;


    public string LevelUpAgilityName => _levelUpAgility.Label;
    public string LevelUpKnowledgeName => _levelUpKnowledge.Label;
    public string LevelUpStrengthName => _levelUpStrength.Label;
    public int Health => _currentHealth;
    public int Damage => _damage;
    public float AttackRange => _attackRange;

    public event UnityAction<int,int> HealthChanged;
    public event UnityAction<int,int> ExpirienceAdded;
    public event UnityAction MoneyChanged;
    public event UnityAction GotLevelUp;
    public event UnityAction StatsChanged;
    public event UnityAction Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
        MoneyChanged?.Invoke();
    }

    private void Update()
    {
        _currentCooldownRegeneration -= Time.deltaTime;

        if (_currentCooldownRegeneration <= 0 && _currentHealth < _maxHealth)
        {
            _currentHealth += _regeneration;
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            _currentCooldownRegeneration = _cooldownRegeneration;
        }
    } 

    public void AddReward(int money,int expirience)
    {
        Money += money;
        MoneyChanged?.Invoke();
        Experience += expirience;

        if (Experience >= _expirienceToLevelUp)
            LevelUp();

        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
    }

    public void TakeDamage(int damage)
    {
        if (_isInvulnerability == false)
        {
            _currentHealth -= damage;
            HealthChanged?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                string die = "Die";
                _animator.Play(die);
            }
        }
    }

    public int Upgrade(string nameUpgrade)
    {
        int template = 0;

        if (_armorUpgrade.Label == nameUpgrade)
        {
            _maxHealth += _armorUpgrade.UpgradeValue;
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            UpgradeArmor++;
            Money -= _armorUpgrade.Price;
            _countArmorUpgrade--;
            template = _countArmorUpgrade;
        }
        else if (_weaponUpgrade.Label == nameUpgrade)
        {
            _damage += _weaponUpgrade.UpgradeValue;
            UpgradeWeapon++;
            Money -= _weaponUpgrade.Price;
            _countWeaponUpgrade--;
            template = _countWeaponUpgrade;
        }

        MoneyChanged?.Invoke();
        return template;
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

    public void Invulnerability()
    {
        _isInvulnerability = !_isInvulnerability;
    }

    private void LevelUp()
    {
        int experinceToLevelUpInsrease = 50;

        Experience = Experience - _expirienceToLevelUp;
        _expirienceToLevelUp += experinceToLevelUpInsrease;
        Level++;
        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
        GotLevelUp?.Invoke();
    }

    private void Death()
    {
        Dying?.Invoke();
    }

    private void AgilityUp()
    {
        float attackRangeUp = 0.02f;
        _attackRange += attackRangeUp;
    }

    private void StrengthUp()
    {
        _regeneration++;
    }
}
