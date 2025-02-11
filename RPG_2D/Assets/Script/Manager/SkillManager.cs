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
