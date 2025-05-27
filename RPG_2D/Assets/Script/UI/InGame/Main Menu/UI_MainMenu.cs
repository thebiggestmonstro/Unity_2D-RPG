using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] 
    private string _sceneName = "MainScene";
    [SerializeField] 
    private GameObject _continueButton;

    private void Start()
    {
        if (GameSaveManager._gameSaveManagerinstance.FindSavedData() == false)
            _continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void NewGame()
    {
        GameSaveManager._gameSaveManagerinstance.DeleteSavedData();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
