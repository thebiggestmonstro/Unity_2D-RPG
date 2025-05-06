using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIManager _uiManager;
    public bool _isSkillUnlocked;

    [SerializeField]
    private UI_SkillTreeSlot[] _slotsToUnlock;
    [SerializeField]
    private UI_SkillTreeSlot[] _slotsToLock;

    private Image _skillImage;

    [SerializeField]
    private string _skillName;
    [TextArea]
    [SerializeField]
    private string _skillDescription;

    [SerializeField]
    private Color _lockedSkillColor;

    private void OnValidate()
    {
        gameObject.name = "UI_SkillTreeSlot : " + _skillName;
    }

    private void Start()
    {
        _uiManager = GetComponentInParent<UIManager>();

        _skillImage = GetComponent<Image>();
        _skillImage.color = _lockedSkillColor;

        GetComponent<Button>().onClick.AddListener(() => UnlcokSkillSlot());

        _uiManager._skillToolTip.HideToolTip();
    }

    public void UnlcokSkillSlot()
    {
        for (int i = 0; i < _slotsToUnlock.Length; i++)
        {
            if (_slotsToUnlock[i]._isSkillUnlocked == false)
                return;
        }

        for (int i = 0; i < _slotsToLock.Length; i++)
        {
            if (_slotsToLock[i]._isSkillUnlocked == true)
                return;
        }

        _isSkillUnlocked = true;
        _skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _uiManager._skillToolTip.ShowToolTip(_skillDescription, _skillName);


        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 960)
            xOffset = -150;
        else
            xOffset = 150;

        if (mousePosition.y > 540)
            yOffset = -150;
        else
            yOffset = 150;

        _uiManager._skillToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiManager._skillToolTip.HideToolTip();
    }
}
