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
    [SerializeField] 
    UI_FadeScreen _fadeScreen;


    private void Start()
    {
        if (GameSaveManager._gameSaveManagerinstance.FindSavedData() == false)
            _continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        GameSaveManager._gameSaveManagerinstance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void ExitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator LoadSceneWithFadeEffect(float delay)
    {
        _fadeScreen.FadeOut();

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(_sceneName);
    }
}
