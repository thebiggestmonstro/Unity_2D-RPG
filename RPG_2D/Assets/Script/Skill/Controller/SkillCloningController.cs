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

    public void DoSetupClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 offset)
    {
        if (canAttack)
            _animator.SetInteger("AttackNumber", Random.Range(1, 3));

        gameObject.transform.position = newTransform.position + offset;
        _cloneTimer = cloneDuration;

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
                hit.GetComponent<EnemyController>().DoGetDamage();
        }
    }

    private void DoFaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 25f);

        float closestDistance = Mathf.Infinity;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                float distanceToEnemy = Vector2.Distance(gameObject.transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    _closestEnemy = hit.transform;
                }
                    
            }
        }

        // 기억된 적을 대상으로
        if (_closestEnemy != null)
        {
            // 적이 나보다 x축 기준으로 뒤에 있다면 방향 회전
            if (gameObject.transform.position.x > _closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
