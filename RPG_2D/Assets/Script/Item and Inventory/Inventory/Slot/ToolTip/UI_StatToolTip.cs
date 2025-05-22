using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatToolTip : UI_ToolTip
{
    [SerializeField] 
    private TextMeshProUGUI _statDescription;

    public void ShowStatToolTip(string _text)
    {
        _statDescription.text = _text;
        AdjustPosition();

        gameObject.SetActive(true);
    }

    public void HideStatToolTip()
    {
        _statDescription.text = "";
        gameObject.SetActive(false);
    }
}
