using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager instance;

    [SerializeField] private GameObject uiVictory;
    [SerializeField] private GameObject uiDefeat;
    
    private bool isGameOver = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void EndGame(bool didPlayerWin)
    {
        isGameOver = true;

        if(!didPlayerWin)
        {
            GridManager.instance.RevealBombs();
        }

        OpenEndGameScreen(didPlayerWin);
    }

    private void OpenEndGameScreen(bool didPlayerWin)
    {
        if(didPlayerWin)
        {
            SetActiveOfUI(uiVictory, true);
        }
        else
        {
            SetActiveOfUI(uiDefeat, true);
        }
    }

    private void SetActiveOfUI(GameObject uiToSetActive, bool setActive)
    {
        uiToSetActive.SetActive(setActive);
    }
}
