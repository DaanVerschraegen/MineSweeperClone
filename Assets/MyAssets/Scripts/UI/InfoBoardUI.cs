using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBoardUI : MonoBehaviour
{
    public static InfoBoardUI instance;

    [SerializeField] private TextMeshProUGUI txtBombsRemaining;
    [SerializeField] private TextMeshProUGUI txtTimePassed;
    [SerializeField] private Button btnResetGame;

    private int bombsRemaining = 0;
    private float timePassed = 0f;
    private bool firstBombSelected = false;

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

        UpdateTextTimePassed();
    }

    private void Update()
    {
        if(firstBombSelected)
        {
            timePassed += Time.deltaTime;
            UpdateTextTimePassed();
        }
    }

    public void SetAmountBombsRemaining(int amountBombs)
    {
        bombsRemaining = amountBombs;
        UpdateTextBombsRemaining();
    }

    public void AddBombsRemaining(int amountBombsToAdd)
    {
        bombsRemaining += amountBombsToAdd;
        UpdateTextBombsRemaining();
    }

    public bool IsFirstBombSelected()
    {
        return firstBombSelected;
    }

    public void SetFirstBombSelected(bool isFirstBombSelected)
    {
        firstBombSelected = isFirstBombSelected;
    }

    private void UpdateTextBombsRemaining()
    {
        txtBombsRemaining.text = bombsRemaining.ToString();
    }

    private void UpdateTextTimePassed()
    {
        txtTimePassed.text = timePassed.ToString("0");
    }

}
