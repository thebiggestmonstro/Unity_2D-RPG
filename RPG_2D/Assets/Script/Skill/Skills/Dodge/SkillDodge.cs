using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDodge : SkillTemplate
{
    [Header("Dodge")]
    [SerializeField] 
    private UI_SkillTreeSlot _unlockDodgeButton;
    [SerializeField] 
    private int _percentageOfEvasion;
    public bool _isDodgeUnlocked;

    [Header("Mirage dodge")]
    [SerializeField] 
    private UI_SkillTreeSlot _unlockMirageDodgeButton;
    public bool _isDodgeMirageUnlocked;


    protected override void Start()
    {
        base.Start();

        _unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        _unlockMirageDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockMirageDodge);
    }


    private void UnlockDodge()
    {
        if (_unlockDodgeButton._isSkillUnlocked && !_isDodgeUnlocked)
        {
            _playerController._characterStats._evasion.AddModifier(_percentageOfEvasion);
            InventoryManager._inventoryManagerInstance.UpdateStatsUI();
            _isDodgeUnlocked = true;
        }
    }

    private void UnlockMirageDodge()
    {
        if (_unlockMirageDodgeButton._isSkillUnlocked)
            _isDodgeMirageUnlocked = true;
    }

    public void CreateMirageOnDodge()
    {
        if (_isDodgeMirageUnlocked)
            SkillManager._skillManagerInstance._skillCloning.DoCreateClone(_playerController.transform, new Vector3(2 * _playerController._facingDir, 0));
    }
}
