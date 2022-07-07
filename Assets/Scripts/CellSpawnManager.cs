using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawnManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject spawnObject;
    public GameObject[,] cellsArray;

    public int columnsCount, rowsCount;
    public float cellMargin;

    void Start()
    {

        CreateTable(columnsCount, rowsCount);

    }

    public void CreateTable(int columnsCount, int rowsCount)
    {
        cellsArray = new GameObject[columnsCount, rowsCount];
        for (int i = 0; i < columnsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                Vector3 startPos = FindStartPosition(columnsCount, rowsCount);

                var shiftFromStart = new Vector3(startPos.x + i * (cellMargin + 1), startPos.y - j * (cellMargin + 1), 0);

                var newCell = Instantiate(cellPrefab, shiftFromStart, cellPrefab.transform.rotation);

                newCell.transform.parent = spawnObject.transform;
                cellsArray[i, j] = newCell;
            }
        }

    }
    public Vector3 FindStartPosition(float columnsCount, float rowsCount)
    {
        float startPosX = columnsCount >= 2 ? -(columnsCount - 1) / 2 - (columnsCount - 1) / 2 * cellMargin : 0;
        float startPosY = rowsCount >= 2 ? (rowsCount - 1) / 2 + (rowsCount - 1) / 2 * cellMargin : 0;
        return new Vector3(startPosX, startPosY);
    }

    public Vector3 FindCellPosition(int xIndex, int yIndex)
    {
        return cellsArray[xIndex, yIndex].transform.position;
    }

    public (int, int) FindCellIndex(Vector3 cellPosition)
    {
        int columnsCount = cellsArray.GetUpperBound(0)+1;
        int rowsCount = cellsArray.GetUpperBound(1)+1;

        Vector3 startPos = FindStartPosition(columnsCount, rowsCount);

        for (int i = 0; i < columnsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                var shiftFromStar = new Vector3(startPos.x + i * (cellMargin + 1), startPos.y - j * (cellMargin + 1), 0);
                if (shiftFromStar == cellPosition)
                {
                    return (i, j);
                }
                
            }
        }
        return (-1, -1);
    }
}
