using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SkillCloning : SkillTemplate
{
    [Header("Clone Info")]
    [SerializeField]
    private GameObject _clonePrefab;
    [SerializeField]
    private float _cloneDuration;

    [SerializeField]
    private bool _canAttack;

    [SerializeField]
    private bool _canCreateCloneOnCounterAttack;
    [SerializeField]
    private float _CreateDelay;

    [Header("Clone Duplication")]
    [SerializeField]
    private bool _canDuplicateClone;
    [SerializeField]
    private float _chanceToDuplicate;

    [Header("Crystal instead of Clone")]
    public bool _canCreateCrystalInsteadOfClone;

    public void DoCreateClone(Transform clonePosition, Vector3 offset)
    {
        if (_canCreateCrystalInsteadOfClone)
        {
            SkillManager._skillManagerInstance._skillCrystal.CreateCrystal();
            SkillManager._skillManagerInstance._skillCrystal.CurrentCrystalChooseRandomtarget();
            return;
        }

        GameObject newClone = Instantiate(_clonePrefab);

        newClone.GetComponent<SkillCloningController>().DoSetupClone(
            clonePosition, 
            _cloneDuration, 
            _canAttack, 
            offset, 
            FindClosestEnemy(newClone.transform),
            _canDuplicateClone,
            _chanceToDuplicate,
            _playerController
        );
    }

    public void CreateCloneOnCounterAttack(Transform enemyTrasform)
    {
        if (_canCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(enemyTrasform, new Vector3(2 * _playerController._facingDir, 0, 0)));
    }

    private IEnumerator CreateCloneWithDelay(Transform enemyTrasform, Vector3 offset)
    {
        yield return new WaitForSeconds(_CreateDelay);
        DoCreateClone(enemyTrasform, offset);
    }
}