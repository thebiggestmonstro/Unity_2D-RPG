using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField]
    protected Transform _groundCheck;
    [SerializeField]
    protected float _groundCheckDistance;
    [SerializeField]
    protected Transform _wallCheck;
    [SerializeField]
    protected float _wallCheckDistance;
    [SerializeField] 
    protected LayerMask _layerOfGround;

    [Header("Attack Collision Info")]
    public Transform _attackCheck;
    public float _attackCheckRadius;

    [Header("Knockback Info")]
    [SerializeField]
    protected Vector2 _knockbackPower;
    protected bool _isKnocked;
    [SerializeField]
    float _knockbackDuration;

    public Animator _animator { get; private set; }
    public Rigidbody2D _rigidbody2D { get; private set; }
    public SpriteRenderer _spriteRenderer { get; private set; }
    public BaseCharacterStats _characterStats { get; private set; }
    public BaseEffectController _baseEffectController { get;  private set; }
    public CapsuleCollider2D _capsuleCollider { get; private set; }

    public int _facingDir { get; set; } = 1;
    protected bool _facingRight = true;

    public System.Action onFlipped;

    public int _knockbackDir { get; private set; }

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _baseEffectController = GetComponent<BaseEffectController>();
        _characterStats = GetComponent<BaseCharacterStats>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    { 
    
    }

    public virtual void SetVelocity(float xVelocity, float yVelcoity)
    {
        if (_isKnocked)
            return;

        _rigidbody2D.velocity = new Vector2(xVelocity, yVelcoity);
        DoFlip(xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        if (_isKnocked)
            return;

        _rigidbody2D.velocity = Vector2.zero;
    }

    public virtual void Flip()
    {
        _facingDir *= -1;
        _facingRight = !_facingRight;
        gameObject.transform.Rotate(0, 180, 0);

        onFlipped();
    }

    public virtual void DoFlip(float xParam)
    {
        if (xParam > 0 && !_facingRight)
            Flip();
        else if (xParam < 0 && _facingRight)
            Flip();
    }

    public virtual bool DoDetectIsGrounded() => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, LayerMask.GetMask("Ground"));

    public virtual bool DoDetectIsFacingWall() => Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDir, _wallCheckDistance, LayerMask.GetMask("Ground"));

    public virtual void DoGetDamage() => StartCoroutine("DoGetKnockbacked");

    protected virtual IEnumerator DoGetKnockbacked()
    {
        _isKnocked = true;

        _rigidbody2D.velocity = new Vector2(_knockbackPower.x * _knockbackDir, _knockbackPower.y);

        yield return new WaitForSeconds(_knockbackDuration);

        _isKnocked = false;

        SetupZeroKnockbackPower();
    }

    public virtual void SetupKnockbackDir(Transform damageDirection)
    {
        if (damageDirection.position.x > transform.position.x)
            _knockbackDir = -1;
        else if (damageDirection.position.x < transform.position.x)
            _knockbackDir = 1;
    }

    protected virtual void SetupZeroKnockbackPower()
    {

    }

    public void SetupKnockbackPower(Vector2 knockbackpower) => _knockbackPower = knockbackpower;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            _groundCheck.position,
            new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance)
        );

        Gizmos.DrawLine(
            _wallCheck.position,
            new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y)
        );

        Gizmos.DrawWireSphere(
            _attackCheck.position,
            _attackCheckRadius
        );    
    }

    public virtual void Die()
    { 
    
    }

    public virtual void MakeCharacterSlow(float slowPercentage, float slowDuration)
    { 
        
    }

    protected virtual void RollbackDefaultSpeed()
    {
        _animator.speed = 1;
    }
}
