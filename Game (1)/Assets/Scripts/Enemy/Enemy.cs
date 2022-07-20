using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(StateMachine))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private int _reward;
    [SerializeField] private int _expirience;
    [SerializeField] private float _speed;

    private Player _target;
    private Animator _animator;
    private StateMachine _stateMachine;

    public float Speed => _speed;
    public Player Target => _target;
    public int Reward => _reward;
    public int Expirience => _expirience;
    public Animator Animator => _animator;

    public event UnityAction<Enemy> Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _stateMachine = GetComponent<StateMachine>();
    }

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            string hitAnimation = "TakeHit";

            _health -= damage;
            _animator.Play(hitAnimation);
        }

        if (_health <= 0)
        {
            string deathAnimation = "Death";

            Dying?.Invoke(this);
            _animator.Play(deathAnimation);
            _stateMachine.enabled = false;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}