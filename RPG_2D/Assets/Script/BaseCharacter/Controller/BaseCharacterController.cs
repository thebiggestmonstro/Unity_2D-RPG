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

    public Animator _animator { get; set; }
    public Rigidbody2D _rigidbody2D { get; set; }

    public int _facingDir { get; set; } = 1;
    protected bool _facingRight = true;

    protected virtual void Awake()
    { 
    
    }
    
    // Animator와 RigidBody2D를 설정
    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    { 
    
    }

    // 속력을 설정하고 설정한 속력에 따라 방향 회전 수행
    public virtual void SetVelocity(float xVelocity, float yVelcoity)
    {
        _rigidbody2D.velocity = new Vector2(xVelocity, yVelcoity);
        DoFlip(xVelocity);
    }

    // 속력을 0으로 설정
    public virtual void SetZeroVelocity() => _rigidbody2D.velocity = Vector2.zero;

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

    // 땅을 딛고 있는지와 벽을 마주하고 있는지를 Debug Line을 그려 확인
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
    }
}
