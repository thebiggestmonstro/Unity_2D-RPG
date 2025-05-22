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
    [SerializeField]
    private int _skillUnlockCost;

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

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlcokSkillSlot());
    }

    private void Start()
    {
        _uiManager = GetComponentInParent<UIManager>();

        _skillImage = GetComponent<Image>();
        _skillImage.color = _lockedSkillColor;

        _uiManager._skillToolTip.HideToolTip();
    }

    public void UnlcokSkillSlot()
    {
        if (PlayerManager._playerManagerInstance.CanUnlockSkill(_skillUnlockCost) == false)
            return;

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
        _uiManager._skillToolTip.ShowToolTip(_skillDescription, _skillName, _skillUnlockCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _uiManager._skillToolTip.HideToolTip();
    }
}
