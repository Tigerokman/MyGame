using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class FlyingMoveState : State
{
    private Enemy _enemy;
    private float _latestPositionX;
    private string _move = "IsMove";
    private float _colliderNavMesh = 0.05f;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _latestPositionX = _enemy.transform.position.x;
    }

    private void OnEnable()
    {
        _enemy.Dying += Death;
    }

    private void OnDisable()
    {
        _enemy.Dying -= Death;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _enemy.Speed * Time.deltaTime);
        _enemy.Flip(_move, _latestPositionX);
        _latestPositionX = _enemy.transform.position.x;
    }

    private void Death(Enemy enemy)
    {
        this.enabled = false;
    }
}
