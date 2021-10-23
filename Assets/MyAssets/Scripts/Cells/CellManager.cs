using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellManager : MonoBehaviour
{
    private RawImage rawImg;
    private TextMeshProUGUI txtAmountBombsAroundCell;
    private Cell cell;
    private Vector2Int gridPosition;

    private void Awake()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        rawImg = GetComponentInChildren<RawImage>();
        Debug.Assert(rawImg != null, "component RawImage not found.");

        txtAmountBombsAroundCell = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Assert(txtAmountBombsAroundCell != null, "component TextMeshProUGUI not found.");

        cell = new Cell();
        UpdateCellColor();
    }

    public Cell GetCell()
    {
        return cell;
    }

    //This method only executes when CellStatus is either "Selected" or "Hidden"
    public void ChangeSelectedCell(bool selected)
    {
        if(cell.GetCellStatus() == CellStatus.Selected || cell.GetCellStatus() == CellStatus.Hidden)
        {
            if(selected)
            {
                cell.UpdateCellStatus(CellStatus.Selected);
            }
            else
            {
                cell.UpdateCellStatus(CellStatus.Hidden);
            }

            UpdateCellColor();
        }
    }

    public void RevealCell()
    {
        //Do not execute this method if the cellstatus is already visible
        if(cell.GetCellStatus() == CellStatus.Visible)
        {
            return;
        }

        cell.UpdateCellStatus(CellStatus.Visible);
        UpdateCellColor();
        CheckIfFirstBombSelected();

        if(!cell.IsBomb())
        {
            UpdateCellText();
            if(!GridManager.instance.AnyNonBombCellsRemaining())
            {
                EndGameManager.instance.EndGame(true);
            }
        }
        else
        {
            ActivateImageBomb();

            if(!EndGameManager.instance.IsGameOver())
            {
                EndGameManager.instance.EndGame(false);
            }
        }
    }

    //Change status to "FlaggedAsBomb" if current status is "Selected" or "Hidden"
    //Change status back to "Selected" if current status is "FlaggedAsBomb"
    //Then UpdateCellColor
    public void FlagCell()
    {
        switch (cell.GetCellStatus())
        {
            case CellStatus.Selected:
            case CellStatus.Hidden:
                cell.UpdateCellStatus(CellStatus.FlaggedAsBomb);
                InfoBoardUI.instance.AddBombsRemaining(-1);
                break;
            case CellStatus.FlaggedAsBomb:
                cell.UpdateCellStatus(CellStatus.Hidden);
                InfoBoardUI.instance.AddBombsRemaining(1);
                break;
        }

        UpdateCellColor();
        CheckIfFirstBombSelected();
    }

    private void CheckIfFirstBombSelected()
    {
        if(!InfoBoardUI.instance.IsFirstBombSelected())
        {
            InfoBoardUI.instance.SetFirstBombSelected(true);
        }
    }

    private void ActivateImageBomb()
    {
        Image imgBomb = GetComponentInChildren<Image>(true);
        imgBomb.transform.gameObject.SetActive(true);
    }

    private void UpdateCellColor()
    {
        rawImg.color = cell.GetCellColor();
    }

    private void UpdateCellText()
    {
        int amountBombs = cell.GetAmountBombsAroundCell();

        if(amountBombs > 0)
        {
            txtAmountBombsAroundCell.text = amountBombs.ToString();
        }
        else
        {
            GridManager.instance.RevealCellsAroundCell(this);
        }
    }
    
    public void SetCellAsBomb()
    {
        cell.SetIsBomb(true);
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public void SetGridPosition(int row, int col)
    {
        gridPosition = new Vector2Int(row, col);
    }
}
