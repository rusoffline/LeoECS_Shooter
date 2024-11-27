using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour, IScreen
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public bool IsActive => winScreen.gameObject.activeSelf || loseScreen.gameObject.activeSelf;

    private void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowGameOver(bool isWin)
    {
        winScreen.SetActive(isWin);
        loseScreen.SetActive(!isWin);
    }

    public void ShowScreen()
    {
    }

    public bool CheckActive()
    {
        return gameObject.activeSelf;
    }

    public bool TryHideScreen()
    {
        return false;
    }
}
