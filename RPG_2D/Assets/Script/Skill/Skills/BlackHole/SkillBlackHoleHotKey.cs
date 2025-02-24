using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillBlackHoleHotKey : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private KeyCode _hotKey;
    private TextMeshProUGUI _myText;

    private Transform _trappedEnemy;
    private SkillBlackHoleController _blackHoleController;

    public void SetupHotKey(KeyCode newHotkey, Transform trappedEnemy, SkillBlackHoleController blackHoleController)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myText = GetComponentInChildren<TextMeshProUGUI>();
       
        _trappedEnemy = trappedEnemy;   
        _blackHoleController = blackHoleController;

        _hotKey = newHotkey;
        _myText.text = newHotkey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_hotKey))
        {
            _blackHoleController.AddTrappedEnemyToList(_trappedEnemy);

            _myText.color = Color.clear;
            _spriteRenderer.color = Color.clear;
        }
    }
}
