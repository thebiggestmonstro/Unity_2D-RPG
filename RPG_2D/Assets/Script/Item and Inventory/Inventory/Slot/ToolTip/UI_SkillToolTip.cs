using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _skillText;
    [SerializeField] 
    private TextMeshProUGUI _skillName;
    [SerializeField] 
    private float _defaultFontSize;

    public void ShowToolTip(string skillDescription, string skillName)
    {
        _skillName.text = skillName;
        _skillText.text = skillDescription;

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        _skillName.fontSize = _defaultFontSize;
        gameObject.SetActive(false);
    }
}
