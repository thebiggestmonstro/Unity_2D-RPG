using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillBlackHoleHotKey : MonoBehaviour
{
    // 입력 키 관련 프로퍼티
    private SpriteRenderer _spriteRenderer;
    private KeyCode _hotKey;
    private TextMeshProUGUI _myText;

    // 스킬 동작 관련 프로퍼티
    private Transform _trappedEnemy;
    private SkillBlackHoleController _blackHoleController;

    // 적이 블랙홀에 묶이는 경우 호출되는 함수
    public void SetupHotKey(KeyCode newHotkey, Transform trappedEnemy, SkillBlackHoleController blackHoleController)
    {
        // 우선, 자기자신과 자식 오브젝트를 탐색하여 입력 키 관련 프로퍼티를 설정
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myText = GetComponentInChildren<TextMeshProUGUI>();
       
        // 블랙홀에 묶인 적과 블랙홀 스킬 동작 주체를 캐싱
        _trappedEnemy = trappedEnemy;   
        _blackHoleController = blackHoleController;

        // 입력키와 생성되는 입력키를 알려줄 UI의 Text를 캐싱
        _hotKey = newHotkey;
        _myText.text = newHotkey.ToString();
    }

    private void Update()
    {
        // 입력키가 알맞게 입력되었다면
        if (Input.GetKeyDown(_hotKey))
        {
            // 묶여있는 적을 공격하기 위해 리스트에 추가
            _blackHoleController.AddTrappedEnemyToList(_trappedEnemy);

            // 입력키를 알맞게 입력하도록 알려주는 UI를 비워줌
            _myText.color = Color.clear;
            _spriteRenderer.color = Color.clear;
        }
    }
}
