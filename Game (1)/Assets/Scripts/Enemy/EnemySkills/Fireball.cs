using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeLife;

    private Transform _player;
    private Rigidbody2D _rigidbody;
    private string _playerTag = "Player";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Vector3 relativePosition = _player.position - transform.position;
        _rigidbody.velocity = relativePosition * _speed;
    }

    private void Update()
    {
        _timeLife -= Time.deltaTime;

        if(_timeLife <= 0 )
            Destroy(gameObject);
    }

    public void Init(Player player)
    {
        _player = player.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
