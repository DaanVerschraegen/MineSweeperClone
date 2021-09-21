using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField] private int rows = 9;
    [SerializeField] private int cols = 9;
    [SerializeField] private float tileSize = 100f;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private int amountBombs = 10;

    private CellManager[,] gridArray;
    private static Vector2Int[] indexOffsetsSurroundingCells = new Vector2Int[]{
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(1, 1)
    };

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            gridArray = new CellManager[rows, cols];
        }
    }

    private void Start()
    {
        GenerateGrid();
        AddBombsToCells();
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
        cellManager.SetGridPosition(row, col);

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

    private void AddBombsToCells()
    {
        for (int i = 0; i < amountBombs; i++)
        {
            CellManager randomCellManager;
            do
            {
                randomCellManager = GetRandomCellFromListCellManagers();
            } while (randomCellManager.GetCell().IsBomb());

            randomCellManager.GetCell().SetIsBomb(true);
            AddBombToCounterBombsAroundCell(randomCellManager);
        }
    }

    private CellManager GetRandomCellFromListCellManagers()
    {
        int randomX = Random.Range(0, rows);
        int randomY = Random.Range(0, cols);
        CellManager randomCellManager = gridArray[randomX, randomY];

        return randomCellManager;
    }

    private void AddBombToCounterBombsAroundCell(CellManager cellManager)
    {
        Vector2Int gridPos = GetGridPositionCellManager(cellManager);

        if(gridPos != null)
        {
            Vector2Int gridPosSurroundingCell;

            foreach (Vector2Int indexOffset in indexOffsetsSurroundingCells)
            {
                gridPosSurroundingCell = OffsetGridPos(gridPos, indexOffset);

                if(IsGridPosInRangeGridArray(gridPosSurroundingCell))
                {
                    gridArray[gridPosSurroundingCell.x, gridPosSurroundingCell.y].GetCell().AddBombsToCounterBombsAroundCell(1);
                }
            }
        }
    }

    public void RevealCellsAroundCell(CellManager cellManager)
    {
        Vector2Int gridPos = GetGridPositionCellManager(cellManager);
        
        if(gridPos != null)
        {
            Vector2Int gridPosSurroundingCell;

            foreach (Vector2Int indexOffset in indexOffsetsSurroundingCells)
            {
                gridPosSurroundingCell = OffsetGridPos(gridPos, indexOffset);

                if(IsGridPosInRangeGridArray(gridPosSurroundingCell))
                {
                    gridArray[gridPosSurroundingCell.x, gridPosSurroundingCell.y].RevealCell();
                }
            }
        }
    }

    public void RevealBombs()
    {
        CellManager[] bombs = (from CellManager cell in gridArray
                                where cell.GetCell().IsBomb()
                                select cell).ToArray();

        foreach (CellManager bomb in bombs)
        {
            bomb.RevealCell();
        }
    }

    public bool AnyNonBombCellsRemaining()
    {
        return (from CellManager cell in gridArray
                where !cell.GetCell().IsBomb()
                && cell.GetCell().GetCellStatus() != CellStatus.Visible
                select cell).Any();
    }

    private Vector2Int GetGridPositionCellManager(CellManager cellManager)
    {
        return cellManager.GetGridPosition();
    }

    private Vector2Int OffsetGridPos(Vector2Int gridPosToOffset, Vector2Int indexOffset)
    {
        return new Vector2Int(gridPosToOffset.x + indexOffset.x, gridPosToOffset.y + indexOffset.y);
    }

    private bool IsGridPosInRangeGridArray(Vector2Int gridPosToCheck)
    {
        return (gridPosToCheck.x >= 0 &&
                gridPosToCheck.x < gridArray.GetLength(0) &&
                gridPosToCheck.y >= 0 &&
                gridPosToCheck.y < gridArray.GetLength(1));
    }
}
