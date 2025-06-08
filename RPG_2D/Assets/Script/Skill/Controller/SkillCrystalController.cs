using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SkillCrystalController : MonoBehaviour
{
    private Animator _crystalAnimator => GetComponent<Animator>();
    private CircleCollider2D _circleColldier => GetComponent<CircleCollider2D>();

    private float _crystalExistTimer;
    private bool _canExplode;
    private bool _canMoveToEnemy;
    private float _moveSpeed;
    private Transform _closestEnemy;
    private PlayerController _playerController;

    private bool _canGrow;
    [SerializeField]
    private float _growSpeed = 5.0f;
    [SerializeField]
    private LayerMask _enemyLayer;

    public void SetupCrystal(float crystalDuration, bool canExplode, bool canMoveToEnemy, float movespeed, Transform closestEnemy, PlayerController playerController)
    {
        _playerController = playerController;
        _crystalExistTimer = crystalDuration;
        _canExplode = canExplode;
        _canMoveToEnemy = canMoveToEnemy;
        _moveSpeed = movespeed;
        _closestEnemy = closestEnemy;
    }

    private void Update()
    {
        _crystalExistTimer -= Time.deltaTime;

        if (_crystalExistTimer < 0)
            FinishExistCrytal();

        if (_canMoveToEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, _closestEnemy.position, _moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _closestEnemy.position) < 1) 
            {
                FinishExistCrytal();
                _canMoveToEnemy = false;
            }
        }

        if (_canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), _growSpeed * Time.deltaTime);
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager._skillManagerInstance._skillBlackHole.GetBlackholeRadius();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, _enemyLayer);

        if(colliders.Length > 0)
            _closestEnemy = colliders[Random.Range(0, colliders.Length)].transform;
    }

    public void FinishExistCrytal()
    {
        if (_canExplode)
        {
            _canGrow = true;
            _crystalAnimator.SetTrigger("Explode");
        }
        else
            DestroyCrystal();
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _circleColldier.radius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<EnemyController>() != null)
            {
                hit.GetComponent<BaseCharacterController>().SetupKnockbackDir(transform);

                _playerController._characterStats.GiveMagicalDamage(hit.GetComponent<BaseCharacterStats>());

                ItemData_Equipment equipedAmulet = InventoryManager._inventoryManagerInstance.GetEquipment(EquipmentType.Amulet);
                if (equipedAmulet)
                    equipedAmulet.ExecuteItemEffect(hit.transform);
            }
        }
    }

    public void DestroyCrystal() => Destroy(gameObject);
}
