using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    // ��� ��ų���� ������ SkillTemplate������ ��ų���� �������� ������Ƽ�� ���� ���� - ��Ÿ��
    [SerializeField]
    protected float _cooldown;
    protected float _cooldownTimer;

    // �ֱ������� _cooldownTimer�� ����
    protected virtual void Update()
    { 
        _cooldownTimer -= Time.deltaTime;
    }

    // ��ų ��� ���� ���θ� �Ǵ��ϴ� �Լ�
    public virtual bool DoDefineCanUseSkill()
    {
        // _cooldownTimer�� 0���� ������ ��ų ��� ����
        if (_cooldownTimer < 0)
        {
            DoUseSkill();
            _cooldownTimer = _cooldown;
            return true;
        }

        Debug.Log("Skill is on cooldown");
        return false;
    }

    // �������̵��� �ڼ� ��ų���� �ڽ��� ������ ������ �� ����ϴ� �Լ�
    public virtual void DoUseSkill()
    { 
        // ��ų �Լ��� ���� ���� 
    }
}
