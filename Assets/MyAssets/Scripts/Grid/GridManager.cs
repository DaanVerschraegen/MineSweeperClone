using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    static public GridManager instance;

    [SerializeField] private int rows = 9;
    [SerializeField] private int cols = 9;
    [SerializeField] private float tileSize = 100f;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private int amountBombs = 10;

    private List<CellManager> listCellManagers;
    [SerializeField] private int[] indexOffsetsSurroundingCells;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            listCellManagers = new List<CellManager>();
            indexOffsetsSurroundingCells = new int[] {-cols - 1, -cols, -cols + 1, -1, 1, cols - 1, cols, cols + 1};
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
        listCellManagers.Add(cellManager);

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
            } while (randomCellManager.getCell().IsBomb());

            randomCellManager.getCell().SetIsBomb(true);
            AddBombToCounterBombsAroundCell(randomCellManager);
        }
    }

    private CellManager GetRandomCellFromListCellManagers()
    {
        int randomIndex = Random.Range(0, listCellManagers.Count);
        CellManager randomCellManager = listCellManagers[randomIndex];

        return randomCellManager;
    }

    private void AddBombToCounterBombsAroundCell(CellManager cellManager)
    {
        int index = GetIndexOfCellManager(cellManager);

        if(index >= 0)
        {
            int indexSurroundingCell;

            foreach (int indexOffset in indexOffsetsSurroundingCells)
            {
                indexSurroundingCell = index + indexOffset;

                if(0 <= indexSurroundingCell && indexSurroundingCell < listCellManagers.Count)
                {
                    listCellManagers[indexSurroundingCell].getCell().AddBombsToCounterBombsAroundCell(1);
                }
            }
        }
    }

    public void RevealCellsAroundCell(CellManager cellManager)
    {
        int index = GetIndexOfCellManager(cellManager);
        
        if(index >= 0)
        {
            int indexSurroundingCell;

            foreach (int indexOffset in indexOffsetsSurroundingCells)
            {
                indexSurroundingCell = index + indexOffset;

                if(0 <= indexSurroundingCell && indexSurroundingCell < listCellManagers.Count)
                {
                    listCellManagers[indexSurroundingCell].RevealCell();
                }
            }
        }
    }

    private int GetIndexOfCellManager(CellManager cellManager)
    {
        return listCellManagers.IndexOf(cellManager);
    }
}
