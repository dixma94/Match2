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
                newCell.row = row;
                newCell.col = col;
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
        return FindCellPosition(columnsCount, rowsCount) + GetShiftFromStart(xIndex, yIndex);
    }

    public CellScript FindCell(Vector2 point)
    {
        Debug.Log(point);

        foreach (var cell in cellsArray)
        {
            bool xStatement = cell.coordinates.x - cellSize / 2 >= point.x || point.x <= cell.coordinates.x + cellSize / 2;
            bool yStatement = cell.coordinates.y - cellSize / 2 >= point.y || point.y <= cell.coordinates.y + cellSize / 2;

            if ( xStatement && yStatement)
            {
                return cell;
            }
        }
        return null;
    }
    void OnMouseDown()
    {
        var cell = FindCell(Input.mousePosition);
        if (cell != null)Debug.Log(cell.row.ToString()+" "+cell.col.ToString());
        if (cell == null) Debug.Log("нет");
    }
}
