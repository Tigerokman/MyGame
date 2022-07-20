using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(StateMachine))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform PointAttack;
    [SerializeField] private float _health;
    [SerializeField] private int _reward;
    [SerializeField] private int _expirience;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _speed;

    protected Fireball Fireball;
    private Player _target;
    private Animator _animator;
    private StateMachine _stateMachine;
    private bool _facingRight = true;

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

    public void Init(Player target, Fireball fireball)
    {
        _target = target;
        Fireball = fireball;
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

    public void Flip(string flipIs, float latestPositionX = 0)
    {
        string FlipAttack = "IsAttack";
        string FlipMove = "IsMove";

        if (flipIs == FlipAttack)
        {
            if (Target.transform.position.x < transform.position.x && _facingRight)
            {
                DoFlip();
            }
            else if (Target.transform.position.x > transform.position.x && !_facingRight)
            {
                DoFlip();
            }
        }
        else if (flipIs == FlipMove)
        {
            if (latestPositionX > transform.position.x && _facingRight)
            {
                DoFlip();
            }
            else if (latestPositionX < transform.position.x && !_facingRight)
            {
                DoFlip();
            }
        }
    }

    protected virtual void Attack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(PointAttack.position, _attackRange);

        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].gameObject.TryGetComponent(out Player enemy))
                enemy.TakeDamage(_damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(PointAttack.position, _attackRange);
    }

    private void DoFlip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}