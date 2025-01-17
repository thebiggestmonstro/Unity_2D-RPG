using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    // 모든 스킬들의 원형인 SkillTemplate에서는 스킬들의 공통적인 프로퍼티를 갖고 있음 - 쿨타임
    [SerializeField]
    protected float _cooldown;
    protected float _cooldownTimer;

    // 주기적으로 _cooldownTimer를 감소
    protected virtual void Update()
    { 
        _cooldownTimer -= Time.deltaTime;
    }

    // 스킬 사용 가능 여부를 판단하는 함수
    public virtual bool DoDefineCanUseSkill()
    {
        // _cooldownTimer가 0보다 작으면 스킬 사용 가능
        if (_cooldownTimer < 0)
        {
            DoUseSkill();
            _cooldownTimer = _cooldown;
            return true;
        }

        Debug.Log("Skill is on cooldown");
        return false;
    }

    // 오버라이드한 자손 스킬들이 자신의 로직을 수행할 때 사용하는 함수
    public virtual void DoUseSkill()
    { 
        // 스킬 함수의 로직 수행 
    }
}
