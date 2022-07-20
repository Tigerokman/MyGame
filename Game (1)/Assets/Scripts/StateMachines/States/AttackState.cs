using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AttackState : State
{
    private string _attack = "IsAttack";
    private Enemy _enemy;

    public void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _enemy.Animator.SetBool(_attack, true);
        _enemy.Dying += Death;
    }

    private void OnDisable()
    {
        _enemy.Animator.SetBool(_attack, false);
        _enemy.Dying += Death;
    }

    private void Update()
    {
        _enemy.Flip(_attack);
    }

    private void Death(Enemy enemy)
    {
        this.enabled = false;
    }
}
