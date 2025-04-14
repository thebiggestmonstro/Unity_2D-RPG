using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField]
    private string _statName;
    [SerializeField]
    private StatType _statType;

    [SerializeField]
    private TextMeshProUGUI _statValueText;
    [SerializeField]
    private TextMeshProUGUI _statNameText;

    private void OnValidate()
    {
        gameObject.name = "Stat : " + _statName;

        if(_statNameText != null)
            _statNameText.text = _statName;
    }

    public void UpdateStatValueUI()
    {
        PlayerStats playerStats = PlayerManager._playerManagerInstance._playerController.GetComponent<PlayerStats>();

        if(playerStats)
            _statValueText.text = playerStats.GetStatByStatType(_statType).GetValue().ToString();
    }

    private void Start()
    {
        UpdateStatValueUI();
    }
}
