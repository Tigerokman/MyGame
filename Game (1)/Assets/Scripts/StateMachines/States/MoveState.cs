using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyActions))]
public class MoveState : State
{
    private NavMeshAgent _agent;
    private Enemy _enemy;
    private EnemyActions _enemyActions;
    private string _move = "IsMove";
    private float _colliderNavMesh = 0.05f;
    private float _latestPositionX;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyActions = GetComponent<EnemyActions>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Start()
    {
        _agent.radius = _colliderNavMesh;
        _latestPositionX = _enemy.transform.position.x;
    }

    private void OnEnable()
    {
        _agent.speed = _enemy.Speed;
        _enemy.Animator.SetBool(_move, true);
        _enemy.Dying += Death;
    }

    private void OnDisable()
    {
        _enemy.Animator.SetBool(_move, false);
        _enemy.Dying -= Death;
        _agent.speed = 0;
    }

    private void Update()
    {
        _agent.SetDestination(Target.transform.position);
        _enemyActions.Flip(_move, _latestPositionX);
        _latestPositionX = _enemy.transform.position.x;
    }

    public void SpeedChange(int speed)
    {
        int maxPercent = 100;
        _agent.speed = _agent.speed * speed / maxPercent;
    }

    private void Death(Enemy enemy)
    {
        _agent.enabled = false;
        this.enabled = false;
    }
}