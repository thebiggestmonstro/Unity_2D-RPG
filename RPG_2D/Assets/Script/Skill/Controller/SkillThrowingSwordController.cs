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

    // Bounce Info
    public bool _isBouncing;
    public int _bounceAmount = 4;
    public float _bounceSpeed;
    public List<Transform> _enemyTargets;
    private int _targetIndex;

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
        _rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
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
                _playerController.CatchTheSword();
        }

        if (_isBouncing && _enemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                _enemyTargets[_targetIndex].position, 
                _bounceSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, _enemyTargets[_targetIndex].position) < 0.1f)
            {
                _targetIndex++;
                _bounceAmount--;

                if (_bounceAmount <= 0)
                { 
                    _isBouncing = false;
                    _isReturning = true;
                }

                if (_targetIndex >= _enemyTargets.Count)
                    _targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isReturning)
            return;

        if (collision.GetComponent<EnemyController>() != null)
        {
            if (_isBouncing && _enemyTargets.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (Collider2D hit in colliders)
                {
                    if (hit.GetComponent<EnemyController>() != null)
                        _enemyTargets.Add(hit.transform);
                }
            }
        }

        SwordStack(collision);
    }

    private void SwordStack(Collider2D collision)
    {
        _canRotate = false;
        _circleCollider2D.enabled = false;

        _rigidBody2D.isKinematic = true;
        _rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        if (_isBouncing && _enemyTargets.Count > 0)
            return;

        _animator.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
