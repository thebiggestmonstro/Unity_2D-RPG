using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager _skillManagerInstance;

    // SkillManager�� ��ų�� �����ϴ� ��ü�̹Ƿ�, �� ��ų�� ���� �˰� �־�� ��
    public SkillDash _skillDash { get; private set; }
    public SkillCloning _skillCloning { get; private set; }
    public SkillThrowingSword _skillThrowingSword { get; private set; }
    public SkillBlackHole _skillBlackHole { get; private set; }

    private void Awake()
    {
        if(_skillManagerInstance != null)
            Destroy(_skillManagerInstance.gameObject);

        _skillManagerInstance = this;
    }

    private void Start()
    {
        _skillDash = GetComponent<SkillDash>();
        _skillCloning = GetComponent<SkillCloning>();
        _skillThrowingSword = GetComponent<SkillThrowingSword>();
        _skillBlackHole = GetComponent<SkillBlackHole>();
    }
}
