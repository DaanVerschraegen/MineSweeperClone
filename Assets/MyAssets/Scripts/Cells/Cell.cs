using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private CellStatus cellStatus;
    private int amountBombsAroundCell;
    private bool isBomb;

    private Color colorCellHidden = Color.gray;
    private Color colorCellSelected = Color.blue;
    private Color colorCellVisible = Color.white;

    public Cell()
    {
        cellStatus = CellStatus.Hidden;
        isBomb = false;
        amountBombsAroundCell = 0;
    }

    //Update cell status will only work as long as the cell is not made visible
    public void UpdateCellStatus(CellStatus cellStatus)
    {
        if(this.cellStatus != CellStatus.Visible)
        {
            this.cellStatus = cellStatus;
        }
    }

    public CellStatus getCellStatus()
    {
        return cellStatus;
    }

    public int GetAmountBombsAroundCell()
    {
        return amountBombsAroundCell;
    }

    public void AddBombsToCounterBombsAroundCell(int amountBombs)
    {
        amountBombsAroundCell += amountBombs;
    }

    public bool IsBomb()
    {
        return isBomb;
    }

    public void SetIsBomb(bool isBomb)
    {
        this.isBomb = isBomb;
    }

    public Color GetCellColor()
    {
        switch (cellStatus)
        {
            case CellStatus.Selected:
                return colorCellSelected;
            case CellStatus.Visible:
                return colorCellVisible;
            default:
                return colorCellHidden;
        }
    }
}
