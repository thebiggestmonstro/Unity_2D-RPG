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

        // 쿨타임이 충족되어 분신술 스킬을 사용가능한 경우
        if (_cloneTimer < 0)
        {
            // 분신을 생성하고, 투명도를 조금씩 낮춤
            _spriteRenderer.color = new Color(1, 1, 1, _spriteRenderer.color.a - (Time.deltaTime * _colorLoosingSpeed));

            // 투명도가 0이 되면 GameObject 파괴
            if (_spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }
    }

    // 분신을 생성
    public void DoSetupClone(Transform newTransform, float cloneDuration, bool _canAttack)
    {
        // 공격할 수 있는 경우, 분신의 Animator 변수를 랜덤하게 설정하여 공격 애니메이션 재생
        if (_canAttack)
            _animator.SetInteger("AttackNumber", Random.Range(1, 3));

        // 분신의 위치 설정 및 쿨타임을 설정
        gameObject.transform.position = newTransform.position;
        _cloneTimer = cloneDuration;

        // 분신이 적을 마주하게끔 설정
        DoFaceClosestTarget();
    }

    // 분신의 애니메이션에 사용될 이벤트 트리거 함수, 쿨타임을 -1로 설정하여 분신술 스킬을 사용가능하게 바꿈 
    private void AnimationTrigger()
    {
        _cloneTimer = -1.0f;
    }

    // 분신의 애니메이션에 사용될 이벤트 트리거 함수
    private void AttackAnimationTrigger()
    {
        // 공격 범위를 설정하고 원 모양으로 오버랩을 수행
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackCheck.position, _attackCheckRadius);

        // 수행 결과를 순회하면서
        foreach (Collider2D hit in colliders)
        {
            // 적이라면 적의 DoGetDamage 함수 호출
            if (hit.GetComponent<EnemyController>() != null)
                hit.GetComponent<EnemyController>().DoGetDamage();
        }
    }

    private void DoFaceClosestTarget()
    {
        // 분신의 위치에서 25 만큼의 반지름으로 원으로 오버랩한후,
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 25f);

        float closestDistance = Mathf.Infinity;

        // 오버랩 결과를 순회
        foreach (var hit in colliders)
        {
            // 오버랩 결과가 적이라면
            if (hit.GetComponent<EnemyController>() != null)
            {
                // 적과의 거리를 저장하고, 적을 기억
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
