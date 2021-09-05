using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellManager : MonoBehaviour
{
    private RawImage img;
    private TextMeshProUGUI txtAmountBombsAroundCell;
    private Cell cell;

    private void Awake()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        img = GetComponentInChildren<RawImage>();
        Debug.Assert(img != null, "component RawImage not found.");

        txtAmountBombsAroundCell = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Assert(txtAmountBombsAroundCell != null, "component TextMeshProUGUI not found.");

        cell = new Cell();
        UpdateCellColor();
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
        cell.UpdateCellStatus(CellStatus.Visible);
        UpdateCellColor();
        UpdateCellText();
    }

    private void UpdateCellColor()
    {
        img.color = cell.GetCellColor();
    }

    private void UpdateCellText()
    {
        txtAmountBombsAroundCell.text = cell.GetAmountBombsAroundCell().ToString();
    }
    
    public void SetCellAsBomb()
    {
        cell.SetIsBomb(true);
    }
}
