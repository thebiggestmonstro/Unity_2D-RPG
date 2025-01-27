using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager _skillManagerInstance;

    // SkillManager는 스킬을 수행하는 주체이므로, 각 스킬에 대해 알고 있어야 함
    public SkillDash _skillDash { get; private set; }
    public SkillCloning _skillCloning { get; private set; }
    public SkillThrowingSword _skillThrowingSword { get; private set; }

    private void Awake()
    {
        // 기존에 SkillManager가 존재한다면, SkillManager를 컴포넌트로 가진 게임 오브젝트 파괴
        if(_skillManagerInstance != null)
            Destroy(_skillManagerInstance.gameObject);

        _skillManagerInstance = this;
    }

    // 스킬을 가져와서 설정
    private void Start()
    {
        _skillDash = GetComponent<SkillDash>();
        _skillCloning = GetComponent<SkillCloning>();
        _skillThrowingSword = GetComponent<SkillThrowingSword>();
    }
}
