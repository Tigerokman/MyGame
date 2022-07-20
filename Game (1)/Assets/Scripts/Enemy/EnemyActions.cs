using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyActions : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] protected Transform PointAttack;

    private Enemy _enemy;
    private bool _facingRight = true;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Flip(string flipIs, float latestPositionX = 0)
    {
        string FlipAttack = "IsAttack";
        string FlipMove = "IsMove";

        if (flipIs == FlipAttack)
        {
            if (_enemy.Target.transform.position.x < transform.position.x && _facingRight)
            {
                DoFlip();
            }
            else if (_enemy.Target.transform.position.x > transform.position.x && !_facingRight)
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

    private void DoFlip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
