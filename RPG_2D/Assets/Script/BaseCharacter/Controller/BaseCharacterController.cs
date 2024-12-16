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

    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    { 
    
    }

    public virtual void SetVelocity(float xVelocity, float yVelcoity)
    {
        _rigidbody2D.velocity = new Vector2(xVelocity, yVelcoity);
        DoFlip(xVelocity);
    }

    public virtual void SetZeroVelocity() => _rigidbody2D.velocity = Vector2.zero;

    public virtual void Flip()
    {
        _facingDir *= -1;
        _facingRight = !_facingRight;
        gameObject.transform.Rotate(0, 180, 0);
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

    protected void OnDrawGizmos()
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
