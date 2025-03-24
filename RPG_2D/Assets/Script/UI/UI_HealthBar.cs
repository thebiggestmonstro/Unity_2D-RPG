using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class U_IHealthBar : MonoBehaviour
{
    private BaseCharacterController _baseCharacterController;
    private BaseCharacterStats _myStats;
    private RectTransform _rectTransform;
    private Slider _slider;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _baseCharacterController = GetComponentInParent<BaseCharacterController>();
        _myStats = GetComponentInParent<BaseCharacterStats>();
        _slider = GetComponentInChildren<Slider>();

        _baseCharacterController.onFlipped += FlipUI;
        _myStats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        _slider.maxValue = _myStats.GetMaxHealthValue();
        _slider.value = _myStats._currentHealth;
    }

    public void FlipUI() => _rectTransform.Rotate(0, 180, 0);
    
    private void OnDisable()
    {
        _baseCharacterController.onFlipped -= FlipUI;
        _myStats.onHealthChanged -= UpdateHealthUI;
    }
}
