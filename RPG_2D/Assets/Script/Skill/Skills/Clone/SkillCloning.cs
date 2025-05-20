using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCloning : SkillTemplate
{
    [Header("Clone Info")]
    [SerializeField]
    private GameObject _clonePrefab;
    [SerializeField]
    private float _cloneDuration;
    [SerializeField]
    private float _CreateDelay = 0.4f;
    [SerializeField]
    private float _cloneBaseAttackPoint;

    [Header("Clone attack")]
    [SerializeField] 
    private UI_SkillTreeSlot _cloneAttackUnlockButton;
    [SerializeField] 
    private float _cloneAttackPoint;
    [SerializeField]
    private bool _canAttack;

    [Header("Aggresive clone")]
    [SerializeField] private UI_SkillTreeSlot _aggresiveCloneUnlockButton;
    [SerializeField] private float _aggresiveCloneAttackPoint;
    public bool _canApplyOnHitEffect { get; private set; }

    [Header("Clone Duplication")]
    [SerializeField]
    private UI_SkillTreeSlot _cloneDuplicateUnlockButton;
    [SerializeField] 
    private float _multiCloneAttackPoint;
    [SerializeField]
    private bool _canDuplicateClone;
    [SerializeField]
    private float _chanceToDuplicate;

    [Header("Crystal instead of Clone")]
    [SerializeField] 
    private UI_SkillTreeSlot _crystalInsteadUnlockButton;
    public bool _canCreateCrystalInsteadOfClone;

    protected override void Start()
    {
        base.Start();

        _cloneAttackUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
        _aggresiveCloneUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAggresiveClone);
        _cloneDuplicateUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultiClone);
        _crystalInsteadUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInstead);
    }

    private void UnlockCloneAttack()
    {
        if (_cloneAttackUnlockButton._isSkillUnlocked)
        {
            _canAttack = true;
            _cloneBaseAttackPoint = _cloneAttackPoint;
        }
    }

    private void UnlockAggresiveClone()
    {
        if (_aggresiveCloneUnlockButton._isSkillUnlocked)
        {
            _canApplyOnHitEffect = true;
            _cloneBaseAttackPoint = _aggresiveCloneAttackPoint;
        }
    }

    private void UnlockMultiClone()
    {
        if (_cloneDuplicateUnlockButton._isSkillUnlocked)
        {
            _canDuplicateClone = true;
            _cloneBaseAttackPoint = _multiCloneAttackPoint;
        }
    }

    private void UnlockCrystalInstead()
    {
        if (_crystalInsteadUnlockButton._isSkillUnlocked)
        {
            _canCreateCrystalInsteadOfClone = true;
        }
    }

    public void DoCreateClone(Transform clonePosition, Vector3 offset)
    {
        if (_canCreateCrystalInsteadOfClone)
        {
            SkillManager._skillManagerInstance._skillCrystal.CreateCrystal();
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
            _playerController,
            _cloneBaseAttackPoint
        );
    }

    public void CreateCloneWithDelay(Transform enemyTrasform)
    {
        StartCoroutine(CreateCloneWithCoroutine(enemyTrasform, new Vector3(2 * _playerController._facingDir, 0, 0)));
    }

    private IEnumerator CreateCloneWithCoroutine(Transform enemyTrasform, Vector3 offset)
    {
        yield return new WaitForSeconds(_CreateDelay);
        DoCreateClone(enemyTrasform, offset);
    }
}