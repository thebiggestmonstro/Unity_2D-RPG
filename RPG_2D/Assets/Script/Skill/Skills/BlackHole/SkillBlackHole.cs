using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBlackHole : SkillTemplate
{
    [SerializeField] 
    private UI_SkillTreeSlot _blackHoleUnlockButton;
    public bool _blackholeUnlocked;
    [SerializeField]
    private GameObject _blackHolePrefab;
    [SerializeField]
    private float _maxSize;
    [SerializeField]
    private float _growSpeed;
    [SerializeField]
    private float _shrinkSpeed;
    [SerializeField]
    private float _blackHoleDuration;
    [Space]
    [SerializeField]
    private int _amountOfAttack;
    [SerializeField]
    private float _cloneAttackCooldown;

    SkillBlackHoleController _currentBlackHoleController;

    protected override void Start()
    {
        base.Start();

        _blackHoleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackhole);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void UnlockBlackhole()
    {
        if (_blackHoleUnlockButton._isSkillUnlocked)
            _blackholeUnlocked = true;
    }

    public virtual bool DoDefineCanUseSkill()
    {
       return base.DoUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackHole = Instantiate(_blackHolePrefab, _playerController.transform.position, Quaternion.identity);
        _currentBlackHoleController = newBlackHole.GetComponent<SkillBlackHoleController>();
        _currentBlackHoleController.SetupBlackHole(_maxSize, _growSpeed, _shrinkSpeed, _amountOfAttack, _cloneAttackCooldown, _blackHoleDuration);

        AudioManager._audioManagerInstance.PlaySFX(18, _playerController.transform);
        AudioManager._audioManagerInstance.PlaySFX(19, _playerController.transform);
    }

    public bool BlackHoleSkillCompleted()
    {
        if (_currentBlackHoleController == null)
            return false;

        if (_currentBlackHoleController._playerCanExitState)
        {
            _currentBlackHoleController = null;
            return true;
        }

        return false;
    }

    public float GetBlackholeRadius()
    {
        return _maxSize / 2;
    }

    protected override void CheckUnlock()
    {
        base.CheckUnlock();

        UnlockBlackhole();
    }
}
