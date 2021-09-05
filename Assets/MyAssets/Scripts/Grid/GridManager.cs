using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int rows = 9;
    [SerializeField] private int cols = 9;
    [SerializeField] private float tileSize = 100f;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private int amountBombs = 10;

    private CellManager[,] gridArray;

    private void Start()
    {
        gridArray = new CellManager[rows, cols];
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                InstantiateTile(row, col);
            }
        }

        UpdateParentPosition();
    }

    private void InstantiateTile(int row, int col)
    {
        GameObject tile = Instantiate(tilePrefab, transformParent);

        CellManager cellManager = tile.GetComponent<CellManager>();
        gridArray[row, col] = cellManager;

        RectTransform tileRectTransform = tile.GetComponent<RectTransform>();
        float posX = col * tileSize;
        float posY = row * -tileSize;
        tileRectTransform.localPosition = new Vector2(posX, posY);
    }

    private void UpdateParentPosition()
    {
        float gridWidth = cols * tileSize;
        float gridHeight = rows * tileSize;
        transformParent.localPosition = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 - tileSize / 2);
    }
}
