using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThrowingSwordController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private CircleCollider2D _circleCollider2D;
    private PlayerController _playerController;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>(); 
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void SetUpSword(Vector2 dir, float gravityScale)
    {
        _rigidBody2D.velocity = dir;
        _rigidBody2D.gravityScale = gravityScale;
    }
}
