using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    [SerializeField] 
    private float _xLimit = 960;
    [SerializeField] 
    private float _yLimit = 540;

    [SerializeField] 
    private float _xOffset = 150;
    [SerializeField] 
    private float _yOffset = 150;

    public virtual void AdjustPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        float newXoffset = 0;
        float newYoffset = 0;

        if (mousePosition.x > _xLimit)
            newXoffset = -_xOffset;
        else
            newXoffset = _xOffset;

        if (mousePosition.y > _yLimit)
            newYoffset = -_yOffset;
        else
            newYoffset = _yOffset;

        transform.position = new Vector2(mousePosition.x + newXoffset, mousePosition.y + newYoffset);
    }

    public void AdjustFontSize(TextMeshProUGUI text)
    {
        if (text.text.Length > 12)
            text.fontSize = text.fontSize * .8f;
    }
}
