using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    [SerializeField]
    public float _cooldown;
    public float _cooldownTimer;

    protected PlayerController _playerController;

    protected virtual void Start()
    {
        _playerController = PlayerManager._playerManagerInstance._playerController;

        CheckUnlock();
    }

    protected virtual void CheckUnlock()
    {

    }

    protected virtual void Update()
    { 
        _cooldownTimer -= Time.deltaTime;
    }

    public virtual bool DoUseSkill()
    {
        if (_cooldownTimer < 0)
        {
            UseSkill();
            _cooldownTimer = _cooldown;
            return true;
        }

        Debug.Log("Skill is on cooldown");
        return false;
    }

    public virtual void UseSkill()
    { 

    }

    protected virtual Transform FindClosestEnemy(Transform checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkTransform.position, 25f);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                float distanceToEnemy = Vector2.Distance(checkTransform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}
