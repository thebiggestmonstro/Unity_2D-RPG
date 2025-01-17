using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : SkillTemplate
{
    public override void DoUseSkill()
    {
        base.DoUseSkill();

        Debug.Log("Use Skill");
    }
}
