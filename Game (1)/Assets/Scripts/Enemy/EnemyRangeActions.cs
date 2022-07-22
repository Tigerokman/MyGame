using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeActions : EnemyActions
{
    [SerializeField] Fireball _fireball;

     protected override void Attack()
    {
        Fireball fireball = Instantiate(_fireball, PointAttack.position, Quaternion.identity);
        fireball.Init(Enemy.Target);
    }
}
