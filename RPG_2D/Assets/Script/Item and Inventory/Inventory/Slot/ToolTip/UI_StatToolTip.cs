using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatToolTip : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _statDescription;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowStatToolTip(string _text)
    {
        _statDescription.text = _text;
        gameObject.SetActive(true);
    }

    public void HideStatToolTip()
    {
        _statDescription.text = "";
        gameObject.SetActive(false);
    }
}
