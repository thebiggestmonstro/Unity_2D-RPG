using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThrowingSwordController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private CircleCollider2D _circleCollider2D;
    private PlayerController _playerController;

    private float _returnSpeed = 12.0f;
    private bool _canRotate = true;
    private bool _isReturning;

    [Header("Bounce Info")]
    private float _bounceSpeed;
    private bool _isBouncing;
    private int _bounceAmount;
    private List<Transform> _enemyTargets;
    private int _targetIndex;

    [Header("Pierce Info")]
    private float _pierceAmount;

    [Header("Spin Info")]
    private float _maxTravelDistance;
    private float _spinDuration;
    private float _spinTimer;
    private bool _wasSpinStopped;
    private bool _isSpinning;

    private float _hitTimer;
    private float _hitCooldown;

    private float _spinDirection;
    private float _freezeTimeDuration;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>(); 
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void DestroySword()
    {
        Destroy(gameObject);
    }

    public void SetupBounceSword(bool isBouncing, int bounceAmount, float bounceSpeed)
    {
        _isBouncing = isBouncing;
        _bounceAmount = bounceAmount;
        _bounceSpeed = bounceSpeed;
        _enemyTargets = new List<Transform>();
    }

    public void SetupPierceSword(int pierceAmount)
    { 
        _pierceAmount = pierceAmount;
    }

    public void SetupSpinSword(bool isSpinning, float maxTravelDistance, float maxDuration, float hitCooldown)
    { 
        _isSpinning = isSpinning;
        _maxTravelDistance = maxTravelDistance;
        _spinDuration = maxDuration;
        _hitCooldown = hitCooldown;
    }

    public void SetUpSword(Vector2 dir, float gravityScale, PlayerController player, float freezeTimeDuration, float returnSpeed)
    {
        _playerController = player;

        _rigidBody2D.velocity = dir;
        _rigidBody2D.gravityScale = gravityScale;

        _freezeTimeDuration = freezeTimeDuration;
        _returnSpeed = returnSpeed;

        if (_pierceAmount <= 0)
            _animator.SetBool("Rotation", true);

        _spinDirection = Mathf.Clamp(_rigidBody2D.velocity.x, -1, 1);

        Invoke("DestroySword", 7.0f);
    }

    public void ReturnSword()
    {
        _rigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        _isReturning = true;
    }

    private void Update()
    {
        if (_canRotate)
            transform.right = _rigidBody2D.velocity;

        if (_isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerController.transform.position, _returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _playerController.transform.position) < 1)
                _playerController.CatchTheSword();
        }

        SwordBounce();
        SwordSpin();
    }

    private void SwordSpin()
    {
        if (_isSpinning)
        {
            if (Vector2.Distance(_playerController.transform.position, transform.position) > _maxTravelDistance && !_wasSpinStopped)
            {
                StopWhenSpinning();
            }

            if (_wasSpinStopped)
            {
                _spinTimer -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(
                    transform.position, 
                    new Vector2(transform.position.x + _spinDirection, transform.position.y), 
                    1.5f * Time.deltaTime
                );

                if (_spinTimer < 0)
                {
                    _isReturning = true;
                    _isSpinning = false;
                }

                _hitTimer -= Time.deltaTime;

                if (_hitTimer < 0)
                {
                    _hitTimer = _hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (Collider2D hit in colliders)
                    {
                        if(hit.GetComponent<EnemyController>())
                            SwordSkillEffect(hit.GetComponent<EnemyController>());
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        _wasSpinStopped = true;
        _rigidBody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        _spinTimer = _spinDuration;
    }

    private void SwordBounce()
    {
        if (_isBouncing && _enemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                _enemyTargets[_targetIndex].position,
                _bounceSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, _enemyTargets[_targetIndex].position) < 0.1f)
            {
                SwordSkillEffect(_enemyTargets[_targetIndex].GetComponent<EnemyController>());

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

        if (collision.GetComponent<EnemyController>())
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            SwordSkillEffect(enemy);
        }

        SetupTargetsForBounce(collision);

        SwordStuck(collision);
    }

    private void SwordSkillEffect(EnemyController enemy)
    {
        enemy.DoGetDamage();
        enemy.StartCoroutine("FreezeEnemyTimer", _freezeTimeDuration);
    }

    private void SetupTargetsForBounce(Collider2D collision)
    {
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
    }

    private void SwordStuck(Collider2D collision)
    {
        if (_pierceAmount > 0 && collision.GetComponent<EnemyController>() != null)
        {
            _pierceAmount--;
            return;
        }

        if (_isSpinning) 
        {
            StopWhenSpinning();
            return;
        }
           
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
