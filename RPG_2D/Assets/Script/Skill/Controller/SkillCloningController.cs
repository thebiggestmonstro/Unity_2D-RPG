using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCloningController : MonoBehaviour
{
    [SerializeField]
    private float _colorLoosingSpeed;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private float _cloneTimer;

    [SerializeField]
    private Transform _attackCheck;
    [SerializeField]
    private float _attackCheckRadius = 0.8f;
    private Transform _closestEnemy;

    private bool _canDuplicateClone;
    private float _chanceToDuplicate;
    private int _facingDir = 1;
    private PlayerController _playerController;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _cloneTimer -= Time.deltaTime;

        if (_cloneTimer < 0)
        {
            _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a - (Time.deltaTime * _colorLoosingSpeed));

            if (_spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void DoSetupClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 offset, Transform closestEnemy, bool canDuplicateClone, float chanceToDuplicate, PlayerController playerController)
    {
        if (canAttack)
            _animator.SetInteger("AttackNumber", Random.Range(1, 3));

        _playerController = playerController;
        gameObject.transform.position = newTransform.position + offset;
        _cloneTimer = cloneDuration;

        _closestEnemy = closestEnemy;
        _canDuplicateClone = canDuplicateClone;
        _chanceToDuplicate = chanceToDuplicate;
        DoFaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        _cloneTimer = -1.0f;
    }

    private void AttackAnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.position, _attackCheckRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                _playerController._characterStats.GiveDamage(hit.GetComponent<BaseCharacterStats>());

                if (_canDuplicateClone)
                {
                    if (Random.Range(0, 100) < _chanceToDuplicate)
                    {
                        SkillManager._skillManagerInstance._skillCloning.DoCreateClone(hit.transform, new Vector3(0.5f * _facingDir, 0));
                    }
                }
            }
        }
    }

    private void DoFaceClosestTarget()
    {
        if (_closestEnemy != null)
        {
            if (gameObject.transform.position.x > _closestEnemy.position.x)
            {
                _facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
