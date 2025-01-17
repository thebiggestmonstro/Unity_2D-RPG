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

    // SkillManager�� ���� �нż��� ����
    public void DoCreateClone(Transform clonePosition, bool canAttack)
    {
        GameObject newClone = Instantiate(_clonePrefab);

        newClone.GetComponent<SkillCloningController>().DoSetupClone(clonePosition, _cloneDuration, canAttack);
    }
}
