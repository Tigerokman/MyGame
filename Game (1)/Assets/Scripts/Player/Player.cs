using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(PlayerStats))]
public class Player : MonoBehaviour
{

    private Animator _animator;
    private PlayerStats _playerStats;
    private bool _isInvulnerability = false;
    private int _currentHealth;
    private int _cooldownRegeneration = 3;
    private int _expirienceToLevelUp = 100;
    private float _currentCooldownRegeneration = 0;

    public int Money { get; private set; } = 0;
    public int Experience { get; private set; } = 0;
    public int Level { get; private set; } = 1;
    public int CurrentHealth => _currentHealth;

    public int Health => _currentHealth;

    public event UnityAction<int,int> HealthChanged;
    public event UnityAction<int,int> ExpirienceAdded;
    public event UnityAction MoneyChanged;
    public event UnityAction GotLevelUp;
    public event UnityAction Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        _currentHealth = _playerStats.MaxHealth;
        HealthChanged?.Invoke(_currentHealth, _playerStats.MaxHealth);
        ExpirienceAdded?.Invoke(Experience, _expirienceToLevelUp);
        MoneyChanged?.Invoke();
        _playerStats.UpgradeByed += Pay;
        _playerStats.MaxHealthIncreased += MaxHealthIncreased;
    }

    private void Update()
    {
        _currentCooldownRegeneration -= Time.deltaTime;

        if (_currentCooldownRegeneration <= 0 && _currentHealth < _playerStats.MaxHealth)
        {
            _currentHealth += _playerStats.Regeneration;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _playerStats.MaxHealth);
            HealthChanged?.Invoke(_currentHealth, _playerStats.MaxHealth);
            _currentCooldownRegeneration = _cooldownRegeneration;
        }
    }

    private void OnDisable()
    {
        _playerStats.UpgradeByed -= Pay;
        _playerStats.MaxHealthIncreased -= MaxHealthIncreased;
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
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _playerStats.MaxHealth);
            HealthChanged?.Invoke(_currentHealth, _playerStats.MaxHealth);

            if (_currentHealth <= 0)
            {
                string die = "Die";
                _animator.Play(die);
            }
        }
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

    private void Pay(int price)
    {
        Money -= price;
        MoneyChanged?.Invoke();
    }

    private void MaxHealthIncreased()
    {
        HealthChanged?.Invoke(_currentHealth, _playerStats.MaxHealth);
    }
}
