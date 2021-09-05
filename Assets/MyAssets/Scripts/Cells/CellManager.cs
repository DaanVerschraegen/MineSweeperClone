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

    public Cell getCell()
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
    }
    
    public void SetCellAsBomb()
    {
        cell.SetIsBomb(true);
    }
}
