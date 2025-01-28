using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThrowingSwordController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private CircleCollider2D _circleCollider2D;
    private PlayerController _playerController;

    [SerializeField]
    private float _returnSpeed = 12.0f;
    private bool _canRotate = true;
    private bool _isReturning;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>(); 
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void SetUpSword(Vector2 dir, float gravityScale, PlayerController player)
    {
        _playerController = player;

        _rigidBody2D.velocity = dir;
        _rigidBody2D.gravityScale = gravityScale;

        _animator.SetBool("Rotation", true);
    }

    public void ReturnSword()
    {
        _rigidBody2D.isKinematic = false;
        transform.parent = null;
        _isReturning = true;
    }

    private void Update()
    {
        if(_canRotate)
            transform.right = _rigidBody2D.velocity;

        if (_isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerController.transform.position, _returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _playerController.transform.position) < 1)
                _playerController.ClearTheSword();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animator.SetBool("Rotation", false);

        _canRotate = false;
        _circleCollider2D.enabled = false;

        _rigidBody2D.isKinematic = true;
        _rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
