using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerLevel))]
public class Player : MonoBehaviour
{

    private Animator _animator;
    private PlayerStats _playerStats;
    private bool _isInvulnerability = false;
    private int _currentHealth;
    private int _cooldownRegeneration = 3;
    private float _currentCooldownRegeneration = 0;

    public int Experience { get; private set; } = 0;
    public int Level { get; private set; } = 1;
    public int CurrentHealth => _currentHealth;

    public int Health => _currentHealth;

    public event UnityAction<int,int> HealthChanged;
    public event UnityAction Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        _currentHealth = _playerStats.MaxHealth;
        HealthChanged?.Invoke(_currentHealth, _playerStats.MaxHealth);
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
        _playerStats.MaxHealthIncreased -= MaxHealthIncreased;
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

    private void Death()
    {
        
        Dying?.Invoke();
    }

    private void MaxHealthIncreased()
    {
        HealthChanged?.Invoke(_currentHealth, _playerStats.MaxHealth);
    }
}
