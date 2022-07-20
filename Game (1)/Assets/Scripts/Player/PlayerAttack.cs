using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(PlayerStats))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private SkillFireWay _skillFireWay;

    private Animator _animator;
    private PlayerInput _playerInput;
    private PlayerStats _player;
    private int _currentAttack = 1;
    private bool _isAttack = false;
    private float _skillCooldown = 8;
    private float _skillCurrentColdown = 0;
    private string _skillName = "Skill";

    public event UnityAction<string> ColldownChange;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerStats>();

        _playerInput.Player.Attack.performed += ctx => OnAttack();
        _playerInput.Player.Skill.performed += ctx => UseSkill();
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnAttack()
    {
        if (_isAttack == false)
        {
            int countAttackAnimate = 3;
            string attack = "Attack";

            IsAttackChange();
            _animator.SetTrigger(attack + _currentAttack);

            if (_currentAttack == countAttackAnimate)
                _currentAttack = 1;
            else
                _currentAttack++;
        }
    }

    private void UseSkill()
    {
        if (_skillCurrentColdown <= 0)
        {
            ColldownChange?.Invoke(_skillName);

            _skillCurrentColdown = _skillCooldown;
            IsAttackChange();
            _animator.SetTrigger(_skillName);
            StartCoroutine(SkillCooldown());
        }
    }

    private void FireWay()
    {
        SkillFireWay skill = Instantiate(_skillFireWay, _attackPoint.position, transform.rotation).GetComponent<SkillFireWay>();
        skill.Init(_player);
    }    

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _player.AttackRange);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_player.Damage);
        }
    }

    private void IsAttackChange()
    {
        _isAttack = !_isAttack;
    }

    private IEnumerator SkillCooldown()
    {
        while (_skillCurrentColdown > 0)
        {
            _skillCurrentColdown -= Time.deltaTime;
            yield return null;
        }

        ColldownChange?.Invoke(_skillName);
        StopCoroutine(SkillCooldown());
    }
}
