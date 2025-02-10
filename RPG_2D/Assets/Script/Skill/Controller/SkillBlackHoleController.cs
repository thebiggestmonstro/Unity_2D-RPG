using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlackHoleController : MonoBehaviour
{
    [SerializeField]
    private GameObject _hotKeyPrefab;
    [SerializeField]
    private List<KeyCode> _keyCodeList;

    public float _maxSize;
    public float _growSpeed;
    public bool _canGrow;

    private List<Transform> _targets = new List<Transform>();

    private void Update()
    {
        // 블랙홀이 커질 수 있다면, 보간을 통해 서서히 크기를 키움
        if (_canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(_maxSize, _maxSize), _growSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger된 대상이 적이라면, 적을 얼리고 적에 대해 입력 키를 생성
        if (collision.GetComponent<EnemyController>() != null)
        {
            collision.GetComponent<EnemyController>().DoFreezeEnemy(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        // 입력 키를 모아놓은 리스트가 비어있다면 얼리 리턴
        if (_keyCodeList.Count == 0)
            return;

        // 적의 머리 위에 새로운 입력 키 오브젝트를 생성
        GameObject newHotKey = Instantiate(_hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        // 입력 키의 경우 입력 키를 모아놓은 리스트에서 랜덤하게 가져옴, 가져온 입력 키의 경우 리스트에서 삭제
        KeyCode chosenHotKey = _keyCodeList[Random.Range(0, _keyCodeList.Count)];
        _keyCodeList.Remove(chosenHotKey);

        // 생성된 입력 키 오브젝트를 대상으로 SetupHotKey 메서드를 호출하여 묶여있는 적에게 입력 키를 설정해줌
        SkillBlackHoleHotKey hotKey = newHotKey.GetComponent<SkillBlackHoleHotKey>();
        hotKey.SetupHotKey(chosenHotKey, collision.transform, this);
    }

    // 공격대상을 모아놓은 리스트에 적을 추가
    public void AddTrappedEnemyToList(Transform trappedEnemy) => _targets.Add(trappedEnemy);
}
