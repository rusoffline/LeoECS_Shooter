using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour, IScreen
{
    public Button gameButton;
    public Button quitButton;
    public int gameSceneIndex;
    public bool IsActive => gameObject.activeSelf;

    public bool CheckActive()
    {
        return gameObject.activeSelf;
    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public bool TryHideScreen()
    {
        gameObject.SetActive(false);
        return true;
    }

    private void Start()
    {
        gameObject.SetActive(false);
        gameButton.onClick.AddListener(() => LoadSceneByIndex(gameSceneIndex));
        quitButton.onClick.AddListener(() => QuitGame());
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
