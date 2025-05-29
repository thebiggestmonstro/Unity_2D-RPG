using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    [SerializeField]
    private BaseCharacterStats _targetStats;
    [SerializeField]
    private float _thunderSpeed;

    private int _damage;
    private Animator _animator;
    private bool _isTriggered;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_targetStats == null)
            return;

        if (_isTriggered)
            return;

        transform.position = Vector2.MoveTowards(transform.position, _targetStats.transform.position, _thunderSpeed * Time.deltaTime);
        transform.right = transform.position - _targetStats.transform.position;

        if (Vector2.Distance(transform.position, _targetStats.transform.position) < 0.1f)
        {
            _animator.transform.localRotation = Quaternion.identity;
            _animator.transform.localPosition = new Vector3(0, 0.5f, 0);
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(3, 3, 3);

            Invoke("GiveDamageAndSelfDestroy", 0.2f);
            _isTriggered = true;
            _animator.SetTrigger("Hit");
        }
    }

    public void SetupThunder(int damage, BaseCharacterStats targetStats)
    {
        _damage = damage;       
        _targetStats = targetStats;
    }

    private void GiveDamageAndSelfDestroy()
    {
        _targetStats.ApplyShock(true);
        _targetStats.TakeDamage(_damage);
        Destroy(gameObject, 0.4f);
    }
}
