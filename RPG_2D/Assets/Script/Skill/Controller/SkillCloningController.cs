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

        // ��Ÿ���� �����Ǿ� �нż� ��ų�� ��밡���� ���
        if (_cloneTimer < 0)
        {
            // �н��� �����ϰ�, ������ ���ݾ� ����
            _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a - (Time.deltaTime * _colorLoosingSpeed));

            // ������ 0�� �Ǹ� GameObject �ı�
            if (_spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }
    }

    // �н��� ����
    public void DoSetupClone(Transform newTransform, float cloneDuration, bool _canAttack)
    {
        // ������ �� �ִ� ���, �н��� Animator ������ �����ϰ� �����Ͽ� ���� �ִϸ��̼� ���
        if (_canAttack)
            _animator.SetInteger("AttackNumber", Random.Range(1, 3));

        // �н��� ��ġ ���� �� ��Ÿ���� ����
        gameObject.transform.position = newTransform.position;
        _cloneTimer = cloneDuration;

        // �н��� ���� �����ϰԲ� ����
        DoFaceClosestTarget();
    }

    // �н��� �ִϸ��̼ǿ� ���� �̺�Ʈ Ʈ���� �Լ�, ��Ÿ���� -1�� �����Ͽ� �нż� ��ų�� ��밡���ϰ� �ٲ� 
    private void AnimationTrigger()
    {
        _cloneTimer = -1.0f;
    }

    // �н��� �ִϸ��̼ǿ� ���� �̺�Ʈ Ʈ���� �Լ�
    private void AttackAnimationTrigger()
    {
        // ���� ������ �����ϰ� �� ������� �������� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.position, _attackCheckRadius);

        // ���� ����� ��ȸ�ϸ鼭
        foreach (Collider2D hit in colliders)
        {
            // ���̶�� ���� DoGetDamage �Լ� ȣ��
            if (hit.GetComponent<EnemyController>() != null)
                hit.GetComponent<EnemyController>().DoGetDamage();
        }
    }

    private void DoFaceClosestTarget()
    {
        // �н��� ��ġ���� 25 ��ŭ�� ���������� ������ ����������,
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 25f);

        float closestDistance = Mathf.Infinity;

        // ������ ����� ��ȸ
        foreach (var hit in colliders)
        {
            // ������ ����� ���̶��
            if (hit.GetComponent<EnemyController>() != null)
            {
                // ������ �Ÿ��� �����ϰ�, ���� ���
                float distanceToEnemy = Vector2.Distance(gameObject.transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    _closestEnemy = hit.transform;
                }
                    
            }
        }

        // ���� ���� �������
        if (_closestEnemy != null)
        {
            // ���� ������ x�� �������� �ڿ� �ִٸ� ���� ȸ��
            if (gameObject.transform.position.x > _closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
