using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIManager _uiManager;

    [SerializeField]
    private string _statName;
    [SerializeField]
    private StatType _statType;

    [SerializeField]
    private TextMeshProUGUI _statValueText;
    [SerializeField]
    private TextMeshProUGUI _statNameText;

    [TextArea]
    [SerializeField] 
    private string _statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat : " + _statName;

        if(_statNameText != null)
            _statNameText.text = _statName;
    }

    private void Start()
    {
        _uiManager = GetComponentInParent<UIManager>();
        UpdateStatValueUI();
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();

        if (playerStats)
        {
            _statValueText.text = playerStats.GetStatByStatType(_statType).GetValue().ToString();

            if (_statType == StatType._maxHealth)
                _statValueText.text = playerStats.GetMaxHealthValue().ToString();

            if (_statType == StatType._attackPoint)
                _statValueText.text = (playerStats._attackPoint.GetValue() + playerStats._strength.GetValue()).ToString();

            if (_statType == StatType._critPower)
                _statValueText.text = (playerStats._critPower.GetValue() + playerStats._strength.GetValue()).ToString();

            if (_statType == StatType._critChance)
                _statValueText.text = (playerStats._critChance.GetValue() + playerStats._agility.GetValue()).ToString();

            if (_statType == StatType._evasion)
                _statValueText.text = (playerStats._evasion.GetValue() + playerStats._agility.GetValue()).ToString();

            if (_statType == StatType._magicResistance)
                _statValueText.text = (playerStats._magicResistance.GetValue() + (playerStats._intelligence.GetValue() * 3)).ToString();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiManager._statToolTip.ShowStatToolTip(_statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiManager._statToolTip.HideStatToolTip();
    }
}
