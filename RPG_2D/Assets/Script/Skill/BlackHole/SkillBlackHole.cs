using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlackHole : SkillTemplate
{
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

    protected virtual void Start()
    {
        base.Start();
    }

    protected virtual void Update()
    {
        base.Update();
    }

    public virtual bool DoDefineCanUseSkill()
    {
       return base.DoDefineCanUseSkill();
    }

    public virtual void DoUseSkill()
    {
        base.DoUseSkill();

        GameObject newBlackHole = Instantiate(_blackHolePrefab, _playerController.transform.position, Quaternion.identity);
        _currentBlackHoleController = newBlackHole.GetComponent<SkillBlackHoleController>();
        _currentBlackHoleController.SetupBlackHole(_maxSize, _growSpeed, _shrinkSpeed, _amountOfAttack, _cloneAttackCooldown, _blackHoleDuration);
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
}
