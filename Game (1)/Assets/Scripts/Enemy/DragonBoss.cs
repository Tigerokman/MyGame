using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonBoss : Enemy
{
    protected override void Attack()
    {
        Instantiate(Fireball, PointAttack.position, Quaternion.identity);
    }
}
