using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDash : SkillTemplate
{
    [Header("Dash")]
    public bool _dashUnlocked;
    [SerializeField]
    private UI_SkillTreeSlot _dashUnlockButton;

    [Header("Clone on Dash")]
    public bool _cloneOnDashUnlocked;
    [SerializeField]
    private UI_SkillTreeSlot _cloneOnDashUnlockButton;

    [Header("Clone on Arrival")]
    public bool _cloneOnArrivalUnlocked;
    [SerializeField]
    private UI_SkillTreeSlot _cloneOnArrivalUnlockButton;

    protected override void Start()
    {
        base.Start();

        _dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        _cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        _cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }

    private void UnlockDash()
    {
        if(_dashUnlockButton._isSkillUnlocked)
            _dashUnlocked = true;
    }

    private void UnlockCloneOnDash()
    {
        if (_cloneOnDashUnlockButton._isSkillUnlocked)
            _cloneOnDashUnlocked = true;
    }

    private void UnlockCloneOnArrival()
    {
        if (_cloneOnArrivalUnlockButton._isSkillUnlocked)
            _cloneOnArrivalUnlocked = true;
    }

    public void CreateCloneOnDashStart()
    {
        if (_cloneOnDashUnlocked)
            SkillManager._skillManagerInstance._skillCloning.DoCreateClone(_playerController.transform, Vector3.zero);
    }

    public void CreateCloneOnDashOver()
    {
        if (_cloneOnArrivalUnlocked)
            SkillManager._skillManagerInstance._skillCloning.DoCreateClone(_playerController.transform, Vector3.zero);
    }

    protected override void CheckUnlock()
    {
        UnlockDash();
        UnlockCloneOnDash();
        UnlockCloneOnArrival();
    }
}
