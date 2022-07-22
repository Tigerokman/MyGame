using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class PointAttackDirection : MonoBehaviour
{
    [SerializeField] Transform _pointAttack;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Vector3 relativePosition = _enemy.Target.transform.position - _pointAttack.position;
        Debug.DrawRay(_pointAttack.position, relativePosition * 5, Color.red);

        _pointAttack.rotation = Quaternion.LookRotation(relativePosition);
    }
}
