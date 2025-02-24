using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCrystal : SkillTemplate
{
    [SerializeField]
    private GameObject _crsytalPrefab;
    [SerializeField]
    private float _crystalExistTime;
    private GameObject _currentCrsytal;

    [Header("Crystal Mirage")]
    [SerializeField]
    private bool _createCloneInsteadOfCrystal;

    [Header("Explosion Crystal")]
    [SerializeField]
    private bool _canExplode;

    [Header("Moving Crystal")]
    [SerializeField]
    private bool _canMoveToEnemy;
    [SerializeField]
    private float _moveSpeed;

    [Header("Multi Stacking Crystal")]
    [SerializeField]
    private bool _canMakeMultiCrystal;
    [SerializeField]
    private int _amountToAddCrystal;
    [SerializeField]
    private float _multiStackCooldown;
    [SerializeField]
    private float _useTimeWindow;
    [SerializeField]
    private List<GameObject> _crystalList = new List<GameObject>();

    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrystal())
            return;

        if (_currentCrsytal == null)
        {
            CreateCrystal();
        }
        else
        {
            if (_canMoveToEnemy)
                return;

            UnityEngine.Vector2 playerPos = _playerController.transform.position;
            _playerController.transform.position = _currentCrsytal.transform.position;
            _currentCrsytal.transform.position = playerPos;

            if (_createCloneInsteadOfCrystal)
            {
                SkillManager._skillManagerInstance._skillCloning.DoCreateClone(_currentCrsytal.transform, Vector3.zero);
                Destroy(_currentCrsytal);
            }
            else
            {
                _currentCrsytal.GetComponent<SkillCrystalController>()?.FinishExistCrytal();
            }
        }
    }

    public void CreateCrystal()
    {
        _currentCrsytal = Instantiate(_crsytalPrefab, _playerController.transform.position, Quaternion.identity);
        SkillCrystalController currentCrystalController = _currentCrsytal.GetComponent<SkillCrystalController>();
        
        currentCrystalController.SetupCrystal(_crystalExistTime, _canExplode, _canMoveToEnemy, _moveSpeed, FindClosestEnemy(_currentCrsytal.transform));
        currentCrystalController.ChooseRandomEnemy();
    }

    public void CurrentCrystalChooseRandomtarget()
    {
        _currentCrsytal.GetComponent<SkillCrystalController>().ChooseRandomEnemy();
    }

    private bool CanUseMultiCrystal()
    {
        if (_canMakeMultiCrystal)
        {
            if (_crystalList.Count > 0)
            {
                if (_crystalList.Count == 0)
                    Invoke("ResetCrystalAbility", _useTimeWindow);

                _cooldown = 0;
                GameObject crystalToSpawn = _crystalList[_crystalList.Count - 1];
                GameObject newCrystal = Instantiate(crystalToSpawn, _playerController.transform.position, Quaternion.identity);
                _crystalList.Remove(crystalToSpawn);

                newCrystal.GetComponent<SkillCrystalController>()?.SetupCrystal(_crystalExistTime, _canExplode, _canMoveToEnemy, _moveSpeed, FindClosestEnemy(newCrystal.transform));

                if (_crystalList.Count <= 0)
                {
                    _cooldown = _multiStackCooldown;
                    RefillCrystal();
                }

                return true;
            }
        }

        return false;
    }

    private void RefillCrystal()
    {
        int amountToAdd = _amountToAddCrystal - _crystalList.Count;

        for (int i = 0; i < amountToAdd; i++)
        {
            _crystalList.Add(_crsytalPrefab);
        }
    }

    private void ResetCrystalAbility()
    {
        if (_cooldownTimer > 0)
            return;

        _cooldownTimer = _multiStackCooldown;
        RefillCrystal();
    }
}
