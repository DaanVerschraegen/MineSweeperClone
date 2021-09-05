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

    private List<Cell> listCells;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            listCells = new List<Cell>();
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

        Cell cell = tile.GetComponent<CellManager>().getCell();
        listCells.Add(cell);

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
            Cell randomCell;
            do
            {
                randomCell = GetRandomCellFromListCells();
            } while (randomCell.IsBomb());

            randomCell.SetIsBomb(true);
        }
    }

    private Cell GetRandomCellFromListCells()
    {
        int randomIndex = Random.Range(0, listCells.Count);
        Cell randomCell = listCells[randomIndex];

        return randomCell;
    }
}
