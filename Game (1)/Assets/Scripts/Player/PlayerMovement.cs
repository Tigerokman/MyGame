using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _chargeColldown;

    private PlayerInput _playerInput;
    private Vector2 _direction;
    private Player _player;
    private Animator _animator;
    private bool _facingRight = true;
    private float _currentChargeCooldown = 0;
    private string _chargeName = "Charge";

    public event UnityAction<string> ColldownChange;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _playerInput.Player.Charge.performed += ctx => OnCharge();
    }

    private void Update()
    {
        _direction = _playerInput.Player.Movement.ReadValue<Vector2>();

        Move(_direction);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _player.Dying += Death;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _player.Dying -= Death;
    }

    public void SpeedChange(int speed)
    {
        int maxPercent = 100;
        _speed = _speed * speed / maxPercent;
    }

    public void AgilityUp()
    {
        float speedUp = 0.2f;
        float chargeUp = 0.1f;
        _speed += speedUp;
        _chargeColldown -= chargeUp;
    }

    private void OnCharge()
    {
        if (_currentChargeCooldown <= 0)
        {
            ColldownChange?.Invoke(_chargeName);

            _currentChargeCooldown = _chargeColldown;

            _animator.SetTrigger(_chargeName);
            StartCoroutine(ChargeCooldown());
        }
    }

    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = _speed * Time.deltaTime;
        string run = "IsRun";

        Vector3 move = new Vector3(direction.x, direction.y, 0);
        transform.position += move * scaledMoveSpeed;

        Flip();
        _animator.SetBool(run, direction.x != 0 || direction.y != 0);
    }

    private void Flip()
    {
        if (_direction.x < 0 && _facingRight || _direction.x > 0 && !_facingRight)
        {
            _facingRight = !_facingRight;
            transform.Rotate(0f,180f,0f);
        }
    }

    private void Death()
    {
        _speed = 0;
    }

    private IEnumerator ChargeCooldown()
    {
        while (_currentChargeCooldown > 0)
        {
            _currentChargeCooldown -= Time.deltaTime;
            yield return null;
        }

        ColldownChange?.Invoke(_chargeName);
        StopCoroutine(ChargeCooldown());
    }
}
