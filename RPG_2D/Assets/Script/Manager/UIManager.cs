using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _uiManagerInstance;

    [SerializeField]
    private GameObject _characterUI;
    [SerializeField]
    private GameObject _skillTreeUI;
    [SerializeField]
    private GameObject _craftUI;
    [SerializeField]
    private GameObject _optionsUI;
    public UI_CraftWindow _craftWindow;

    public UI_ItemToolTip _itemToolTip;
    public UI_StatToolTip _statToolTip;
    public UI_SkillToolTip _skillToolTip;

    private void Awake()
    {
        if (_uiManagerInstance != null)
            Destroy(_uiManagerInstance.gameObject);

        _uiManagerInstance = this;
        SwitchMenu(_skillTreeUI);
    }

    private void Start()
    {
        SwitchMenu(null);
    }

    public void SwitchCharacterUI()
    {
        SwitchMenuWithKey(_characterUI);
    }

    public void SwitchCraftUI()
    {
        SwitchMenuWithKey(_craftUI);
    }

    public void SwitchSkillTreeUI()
    {
        SwitchMenuWithKey(_skillTreeUI);
    }

    public void SwitchOptionUI() 
    {
        SwitchMenuWithKey(_optionsUI);
    }

    public void SwitchMenu(GameObject menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        { 
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (menu != null)
        {
            menu.SetActive(true);
            if (menu.GetComponentInChildren<UI_ItemToolTip>() && menu.GetComponentInChildren<UI_StatToolTip>())
            { 
                _itemToolTip = menu.GetComponentInChildren<UI_ItemToolTip>();
                _statToolTip = menu.GetComponentInChildren<UI_StatToolTip>();
            }
        }
            
    }

    public void SwitchMenuWithKey(GameObject menu)
    {
        if (menu && menu.activeSelf)
        {
            menu.SetActive(false);
            return;
        }
            
        SwitchMenu(menu);
    }
}
