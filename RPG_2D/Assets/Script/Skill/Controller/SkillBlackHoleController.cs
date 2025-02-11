using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlackHoleController : MonoBehaviour
{
    [SerializeField]
    private GameObject _hotKeyPrefab;
    [SerializeField]
    private List<KeyCode> _keyCodeList;
    private bool _canCreateHotKey = true;

    // Black Hole Info
    private float _maxSize;
    private float _growSpeed;
    private bool _canGrow = true;
    private float _shrinkSpeed;
    private bool _canShrink;
    private float _blackHoleDuration;
    private bool _playerCanDisapear = true;

    // Created Clone Info
    private int _amountOfAttacks = 4;
    private float _cloneAttackCooldown = 0.3f;
    private float _cloneAttackTimer;
    private bool _canCloneAttack;

    private List<Transform> _targets = new List<Transform>();
    private List<GameObject> _createdHotKeys = new List<GameObject>();

    public bool _playerCanExitState { get; private set; }

    public void SetupBlackHole(float maxSize, float growSpeed, float shrinkSpeed, int amountOfAttacks, float cloneAttackCooldown, float blackHoleDuration)
    { 
        _maxSize = maxSize;
        _growSpeed = growSpeed;
        _shrinkSpeed = shrinkSpeed;
        _amountOfAttacks = amountOfAttacks;
        _cloneAttackCooldown = cloneAttackCooldown;
        _blackHoleDuration = blackHoleDuration;
    }

    private void Update()
    {
        _cloneAttackTimer -= Time.deltaTime;
        _blackHoleDuration -= Time.deltaTime;

        if (_blackHoleDuration < 0)
        {
            _blackHoleDuration = Mathf.Infinity;

            if (_targets.Count > 0)
                CloneAttackBegin();
            else
                EndSkillBlackHole();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CloneAttackFinish();
        }

        CloneAttackBegin();

        if (_canGrow && !_canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(_maxSize, _maxSize), _growSpeed * Time.deltaTime);
        }

        if (_canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), _shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    private void CloneAttackFinish()
    {
        if (_targets.Count <= 0)
            return;

        DestroyHotKeys();
        _canCloneAttack = true;
        _canCreateHotKey = false;

        if (_playerCanDisapear)
        {
            _canCreateHotKey = false;
            PlayerManager._playerManagerInstance._playerController.MakeTransparent(true);
        }
    }

    private void CloneAttackBegin()
    {
        if (_cloneAttackTimer < 0 && _canCloneAttack && _amountOfAttacks > 0)
        {
            _cloneAttackTimer = _cloneAttackCooldown;

            int randomIndex = Random.Range(0, _targets.Count);
            float randomXOffset = Random.Range(0, 100) > 50 ? 2 : -2;

            SkillManager._skillManagerInstance._skillCloning.DoCreateClone(_targets[randomIndex], _canCloneAttack, new Vector3(randomXOffset, 0, 0));
            _amountOfAttacks--;

            if (_amountOfAttacks <= 0)
            {
                Invoke("EndSkillBlackHole", 0.5f);
            }
        }
    }

    private void EndSkillBlackHole()
    {
        DestroyHotKeys();
        _playerCanExitState = true;
        _canShrink = true;
        _canCloneAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>() != null)
        {
            collision.GetComponent<EnemyController>().DoFreezeEnemy(true);

            CreateHotKey(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyController>() != null)
            collision.GetComponent<EnemyController>().DoFreezeEnemy(false);
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (_keyCodeList.Count == 0)
            return;
        
        if (_canCreateHotKey == false)
            return;

        GameObject newHotKey = Instantiate(_hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        _createdHotKeys.Add(newHotKey);

        KeyCode chosenHotKey = _keyCodeList[Random.Range(0, _keyCodeList.Count)];
        _keyCodeList.Remove(chosenHotKey);

        SkillBlackHoleHotKey hotKey = newHotKey.GetComponent<SkillBlackHoleHotKey>();
        hotKey.SetupHotKey(chosenHotKey, collision.transform, this);
    }

    private void DestroyHotKeys()
    {
        if (_createdHotKeys.Count <= 0)
            return;

        foreach (GameObject createdHotKey in _createdHotKeys)
            Destroy(createdHotKey);
    }

    public void AddTrappedEnemyToList(Transform trappedEnemy) => _targets.Add(trappedEnemy);
}
