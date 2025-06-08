using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour, ISaveManager
{
    public static UIManager _uiManagerInstance;

    [Header("End screen")]
    [SerializeField] 
    private UI_FadeScreen _fadeScreen;
    [SerializeField] 
    private GameObject _endText;
    [SerializeField] 
    private GameObject _restartButton;
    [Space]

    [SerializeField]
    private GameObject _characterUI;
    [SerializeField]
    private GameObject _skillTreeUI;
    [SerializeField]
    private GameObject _craftUI;
    [SerializeField]
    private GameObject _optionsUI;
    [SerializeField] 
    private GameObject _inGameUI;
    public UI_CraftWindow _craftWindow;

    public UI_ItemToolTip _itemToolTip;
    public UI_StatToolTip _statToolTip;
    public UI_SkillToolTip _skillToolTip;

    [SerializeField]
    private UI_VolumeSlider[] _volumeSettings;

    private void Awake()
    {
        if (_uiManagerInstance != null)
            Destroy(_uiManagerInstance.gameObject);

        _uiManagerInstance = this;
        _fadeScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        SwitchMenu(_skillTreeUI);
        SwitchMenu(_inGameUI);

        _itemToolTip.gameObject.SetActive(false);
        _statToolTip.gameObject.SetActive(false);
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
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;

            if (fadeScreen == false)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (menu != null)
        {
            AudioManager._audioManagerInstance.PlaySFX(7, null);
            menu.SetActive(true);
        }
    }

    public void SwitchMenuWithKey(GameObject menu)
    {
        if (menu && menu.activeSelf)
        {
            menu.SetActive(false);
            CheckInGameUI();
            return;
        }
            
        SwitchMenu(menu);
    }

    private void CheckInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return;
        }

        SwitchMenu(_inGameUI);
    }

    public void SwitchEndScreen()
    {
        _fadeScreen.FadeOut();
        StartCoroutine(EndScreenCorutione());
    }

    IEnumerator EndScreenCorutione()
    {
        yield return new WaitForSeconds(1);
        _endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _restartButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager._gameManagerinstance.RestartScene();

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, float> pair in data._volumeSettings)
        {
            foreach (UI_VolumeSlider eachSlider in _volumeSettings)
            {
                if (eachSlider._parameter == pair.Key)
                    eachSlider.LoadSlider(pair.Value);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        data._volumeSettings.Clear();

        foreach (UI_VolumeSlider eachSlider in _volumeSettings)
        {
            data._volumeSettings.Add(eachSlider._parameter, eachSlider._slider.value);
        }
    }
}
