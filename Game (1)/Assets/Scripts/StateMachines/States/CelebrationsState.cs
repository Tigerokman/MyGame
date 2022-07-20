using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CelebrationsState : State
{
    private Animator _animator;
    private string _idle = "Idle";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(_idle);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
