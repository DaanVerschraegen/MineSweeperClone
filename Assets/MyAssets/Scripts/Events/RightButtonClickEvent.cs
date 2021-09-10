using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButtonClickEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            CellManager cellManager = GetComponent<CellManager>();

            if(cellManager)
            {
                cellManager.RevealCell();
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            CellManager cellManager = GetComponent<CellManager>();

            if(cellManager)
            {
                cellManager.FlagCell();
            }
        }
    }
}
