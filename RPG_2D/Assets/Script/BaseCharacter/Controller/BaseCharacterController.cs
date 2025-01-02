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

    [Header("Attack Collision Info")]
    public Transform _attackCheck;
    public float _attackCheckRadius;

    [Header("Knockback Info")]
    [SerializeField]
    protected Vector2 _knockbackDirection;
    protected bool _isKnocked;
    [SerializeField]
    float _knockbackDuration;

    public Animator _animator { get; private set; }
    public Rigidbody2D _rigidbody2D { get; private set; }

    public BaseEffectController _baseEffectController { get;  private set; }

    public int _facingDir { get; set; } = 1;
    protected bool _facingRight = true;

    protected virtual void Awake()
    { 
    
    }
    
    // 필요한 컴포넌트를 가져옴
    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _baseEffectController = GetComponent<BaseEffectController>();
    }

    protected virtual void Update()
    { 
    
    }

    // 속력을 설정하고 설정한 속력에 따라 방향 회전 수행
    public virtual void SetVelocity(float xVelocity, float yVelcoity)
    {
        // 피격당해 넉백되고 있는 동안 이동을 막음
        if (_isKnocked)
            return;

        _rigidbody2D.velocity = new Vector2(xVelocity, yVelcoity);
        DoFlip(xVelocity);
    }

    // 속력을 0으로 설정
    public virtual void SetZeroVelocity()
    {
        // 피격당해 넉백당하고 있는 동안에는 이동을 고정시키는 것도 막음
        if (_isKnocked)
            return;

        _rigidbody2D.velocity = Vector2.zero;
    }

    // 방향 회전
    public virtual void Flip()
    {
        _facingDir *= -1;
        _facingRight = !_facingRight;
        gameObject.transform.Rotate(0, 180, 0);
    }

    // 방향 회전을 수행
    public virtual void DoFlip(float xParam)
    {
        if (xParam > 0 && !_facingRight)
            Flip();
        else if (xParam < 0 && _facingRight)
            Flip();
    }

    // 땅을 딛고 있는지 확인
    public virtual bool DoDetectIsGrounded() => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, LayerMask.GetMask("Ground"));
    // 벽을 마주하고 있는지 확인
    public virtual bool DoDetectIsFacingWall() => Physics2D.Raycast(_wallCheck.position, Vector2.right * _facingDir, _wallCheckDistance, LayerMask.GetMask("Ground"));

    public virtual void DoGetDamage()
    {
        _baseEffectController.StartCoroutine("DoMakeFlashFX");
        StartCoroutine("DoGetKnockbacked");
    }

    protected virtual IEnumerator DoGetKnockbacked()
    {
        _isKnocked = true;

        _rigidbody2D.velocity = new Vector2(_knockbackDirection.x * -_facingDir, _knockbackDirection.y);

        yield return new WaitForSeconds(_knockbackDuration);

        _isKnocked = false;
    }

    // Coliision 영역을 Debug Line을 그려 확인
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
}
