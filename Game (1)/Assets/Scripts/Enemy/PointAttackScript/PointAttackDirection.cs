using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAttackDirection : MonoBehaviour
{
    private Transform _player;
    private string _playerTag = "Player";

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag).transform;
    }

    private void Update()
    {
        Vector3 relativePosition = _player.position - transform.position;
        Debug.DrawRay(transform.position, relativePosition * 5, Color.red);

        transform.rotation = Quaternion.LookRotation(relativePosition);
    }
}
