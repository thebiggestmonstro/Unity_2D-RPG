using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillBlackHoleHotKey : MonoBehaviour
{
    // �Է� Ű ���� ������Ƽ
    private SpriteRenderer _spriteRenderer;
    private KeyCode _hotKey;
    private TextMeshProUGUI _myText;

    // ��ų ���� ���� ������Ƽ
    private Transform _trappedEnemy;
    private SkillBlackHoleController _blackHoleController;

    // ���� ��Ȧ�� ���̴� ��� ȣ��Ǵ� �Լ�
    public void SetupHotKey(KeyCode newHotkey, Transform trappedEnemy, SkillBlackHoleController blackHoleController)
    {
        // �켱, �ڱ��ڽŰ� �ڽ� ������Ʈ�� Ž���Ͽ� �Է� Ű ���� ������Ƽ�� ����
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myText = GetComponentInChildren<TextMeshProUGUI>();
       
        // ��Ȧ�� ���� ���� ��Ȧ ��ų ���� ��ü�� ĳ��
        _trappedEnemy = trappedEnemy;   
        _blackHoleController = blackHoleController;

        // �Է�Ű�� �����Ǵ� �Է�Ű�� �˷��� UI�� Text�� ĳ��
        _hotKey = newHotkey;
        _myText.text = newHotkey.ToString();
    }

    private void Update()
    {
        // �Է�Ű�� �˸°� �ԷµǾ��ٸ�
        if (Input.GetKeyDown(_hotKey))
        {
            // �����ִ� ���� �����ϱ� ���� ����Ʈ�� �߰�
            _blackHoleController.AddTrappedEnemyToList(_trappedEnemy);

            // �Է�Ű�� �˸°� �Է��ϵ��� �˷��ִ� UI�� �����
            _myText.color = Color.clear;
            _spriteRenderer.color = Color.clear;
        }
    }
}
