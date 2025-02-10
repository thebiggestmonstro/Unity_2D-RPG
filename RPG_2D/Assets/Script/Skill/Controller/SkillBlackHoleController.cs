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
        // ��Ȧ�� Ŀ�� �� �ִٸ�, ������ ���� ������ ũ�⸦ Ű��
        if (_canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(_maxSize, _maxSize), _growSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger�� ����� ���̶��, ���� �󸮰� ���� ���� �Է� Ű�� ����
        if (collision.GetComponent<EnemyController>() != null)
        {
            collision.GetComponent<EnemyController>().DoFreezeEnemy(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        // �Է� Ű�� ��Ƴ��� ����Ʈ�� ����ִٸ� �� ����
        if (_keyCodeList.Count == 0)
            return;

        // ���� �Ӹ� ���� ���ο� �Է� Ű ������Ʈ�� ����
        GameObject newHotKey = Instantiate(_hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        // �Է� Ű�� ��� �Է� Ű�� ��Ƴ��� ����Ʈ���� �����ϰ� ������, ������ �Է� Ű�� ��� ����Ʈ���� ����
        KeyCode chosenHotKey = _keyCodeList[Random.Range(0, _keyCodeList.Count)];
        _keyCodeList.Remove(chosenHotKey);

        // ������ �Է� Ű ������Ʈ�� ������� SetupHotKey �޼��带 ȣ���Ͽ� �����ִ� ������ �Է� Ű�� ��������
        SkillBlackHoleHotKey hotKey = newHotKey.GetComponent<SkillBlackHoleHotKey>();
        hotKey.SetupHotKey(chosenHotKey, collision.transform, this);
    }

    // ���ݴ���� ��Ƴ��� ����Ʈ�� ���� �߰�
    public void AddTrappedEnemyToList(Transform trappedEnemy) => _targets.Add(trappedEnemy);
}
