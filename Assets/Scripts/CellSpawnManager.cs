using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawnManager : MonoBehaviour
{
    public CellScript cellPrefab;
    public GameObject spawnObject;
    public CellScript[,] cellsArray;

    public int columnsCount, rowsCount;
    public float cellMargin;

    public float cellSize = 1;

    void Start()
    {

        CreateTable(columnsCount, rowsCount);

    }

    public void CreateTable(int columnsCount, int rowsCount)
    {
        cellsArray = new CellScript[columnsCount, rowsCount];
        Vector2 startPos = FindStartPosition(columnsCount, rowsCount);
        for (int col = 0; col < columnsCount; col++)
        {
            for (int row = 0; row < rowsCount; row++)
            {

                var shiftFromStart = GetShiftFromStart(col, row);
                var newPosition = startPos + shiftFromStart;
                var newCell = Instantiate<CellScript>(cellPrefab, newPosition, cellPrefab.transform.rotation);
                newCell.transform.parent = spawnObject.transform;
                newCell.coordinates = newPosition;
                cellsArray[col, row] = newCell;
            }
        }

    }

    public Vector2 GetShiftFromStart(int col, int row)
    {
        return new Vector2(col * (cellSize + cellMargin), -row * (cellSize + cellMargin));
    }
    public Vector2 FindStartPosition(float columnsCount, float rowsCount)
    {
        float startPosX =-(columnsCount * cellSize + (columnsCount-1)*cellMargin)/2.0f + cellSize/2;
        float startPosY = (rowsCount * cellSize + (rowsCount - 1) * cellMargin) / 2.0f - cellSize / 2;
        return new Vector2(startPosX, startPosY);
    }

    public Vector2 FindCellPosition(int xIndex, int yIndex)
    {
        return FindStartPosition(columnsCount, rowsCount) + GetShiftFromStart(xIndex, yIndex);
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
