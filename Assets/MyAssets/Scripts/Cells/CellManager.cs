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

    public void ChangeSelectedCell(bool selected)
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

    public void RevealCell()
    {
        //Do not execute this method if the cellstatus is already visible
        if(cell.getCellStatus() == CellStatus.Visible)
        {
            return;
        }

        cell.UpdateCellStatus(CellStatus.Visible);
        UpdateCellColor();

        if(!cell.IsBomb())
        {
            UpdateCellText();
        }
        else
        {
            ActivateImageBomb();
        }
    }

    public void FlagCell()
    {
        cell.UpdateCellStatus(CellStatus.FlaggedAsBomb);
        UpdateCellColor();
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
