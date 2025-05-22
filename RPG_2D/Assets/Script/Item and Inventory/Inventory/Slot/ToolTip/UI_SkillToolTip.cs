using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] 
    private TextMeshProUGUI _skillText;
    [SerializeField] 
    private TextMeshProUGUI _skillName;
    [SerializeField] 
    private TextMeshProUGUI _skillUnlockCost;

    [SerializeField] 
    private float _defaultFontSize;

    public void ShowToolTip(string skillDescription, string skillName, int skillUnlockCost)
    {
        _skillName.text = skillName;
        _skillText.text = skillDescription;
        _skillUnlockCost.text = "Cost: " + skillUnlockCost;

        AdjustPosition();

        AdjustFontSize(_skillName);

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        _skillName.fontSize = _defaultFontSize;
        gameObject.SetActive(false);
    }
}
