using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    private RawImage img;
    private Cell cell;

    private void Awake()
    {
        Instantiate();
    }

    private void Instantiate()
    {
        img = GetComponentInChildren<RawImage>();
        Debug.Assert(img != null, "component RawImage not found.");

        cell = new Cell();
        UpdateCellColor();
    }

    public void ChangeImageSelected(bool selected)
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

    private void UpdateCellColor()
    {
        img.color = cell.GetCellColor();
    }

}
