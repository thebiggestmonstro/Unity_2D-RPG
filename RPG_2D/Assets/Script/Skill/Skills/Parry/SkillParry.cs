using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillParry : SkillTemplate
{
    [Header("Parry")]
    [SerializeField]
    private UI_SkillTreeSlot _parryUnlockButton;
    public bool _parryUnlocked;

    [Header("Parry restore")]
    [SerializeField] private UI_SkillTreeSlot _restoreUnlockButton;
    [Range(0f, 1f)]
    [SerializeField] private float _restoreHealthPerentage;
    public bool _restoreUnlocked;

    [Header("Parry with Clone")]
    [SerializeField] private UI_SkillTreeSlot _parryWithCloneUnlockButton;
    public bool _parryWithCloneUnlocked;

    public override void UseSkill()
    {
        base.UseSkill();

        if (_restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(_playerController._characterStats.GetMaxHealthValue() * _restoreHealthPerentage);
            _playerController._characterStats.HealHealth(restoreAmount);
        }
    }

    protected override void Start()
    {
        base.Start();

        _parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        _restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        _parryWithCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithClone);
    }

    private void UnlockParry()
    {
        if (_parryUnlockButton._isSkillUnlocked)
            _parryUnlocked = true;
    }

    private void UnlockParryRestore()
    {
        if (_restoreUnlockButton._isSkillUnlocked)
            _restoreUnlocked = true;
    }

    private void UnlockParryWithClone()
    {
        if (_parryWithCloneUnlockButton._isSkillUnlocked)
            _parryWithCloneUnlocked = true;
    }

    public void MakeCloneOnParry(Transform _respawnTransform)
    {
        if (_parryWithCloneUnlocked)
            SkillManager._skillManagerInstance._skillCloning.CreateCloneWithDelay(_respawnTransform);
    }

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithClone();
    }
}
