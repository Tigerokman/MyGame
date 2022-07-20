using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkillFireWay : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Rigidbody2D _rigidbody;
    private PlayerStats _player;
    private int _knowledgeDegree = 2;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage + _player.Knowledge * _knowledgeDegree);
        }
        else if (collision.gameObject.TryGetComponent(out Stones stone))
        {
            Destroy(gameObject);
        }
    }

    public void Init(PlayerStats player)
    {
        _player = player;
    }
}
