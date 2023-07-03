using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public Cell cellPrefab;

    public Cell[,] cellsArray;

    public GameObject[] shipsArray;
    public GameObject[] obstacleArray;

    public Action gameOver;

    public int columnsCount, rowsCount;
    public float cellMargin;

    public float cellSize = 1;




    public void CreateTable(int columnsCount, int rowsCount)
    {
        DiscardTable();
        cellsArray = new Cell[columnsCount, rowsCount];
        Vector2 startPos = FindStartPosition(columnsCount, rowsCount);
        for (int col = 0; col < columnsCount; col++)
        {
            for (int row = 0; row < rowsCount; row++)
            {

                var shiftFromStart = GetShiftFromStart(col, row);
                var newPosition = startPos + shiftFromStart;
                var newCell = Instantiate<Cell>(cellPrefab, new Vector3(newPosition.x, newPosition.y, 2), cellPrefab.transform.rotation);
                newCell.transform.name = col.ToString() + " " + row.ToString();
                newCell.transform.parent = transform;
                newCell.coordinates = newPosition;
                newCell.ArrayColIndex = col;
                newCell.ArrayRowIndex = row;
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
        float startPosX = -(columnsCount * cellSize + (columnsCount - 1) * cellMargin) / 2.0f + cellSize / 2;
        float startPosY = (rowsCount * cellSize + (rowsCount - 1) * cellMargin) / 2.0f - cellSize / 2;
        return new Vector2(startPosX + transform.position.x, startPosY + transform.position.y);
    }

    public Vector2 FindCellPosition(int xIndex, int yIndex)
    {
        return FindStartPosition(columnsCount, rowsCount) + GetShiftFromStart(xIndex, yIndex);
    }

    public bool FindCell(Vector2 point, out Cell cellOut)
    {
        foreach (var cell in cellsArray)
        {
            var halfCellZize = cellSize / 2 + cellMargin;
            bool xStatement = cell.coordinates.x - halfCellZize <= point.x && point.x <= cell.coordinates.x + halfCellZize;
            bool yStatement = cell.coordinates.y - halfCellZize <= point.y && point.y <= cell.coordinates.y + halfCellZize;

            if (xStatement && yStatement)
            {
                cellOut = cell;
                return true;
            }
        }
        cellOut = null;
        return false;
    }



    public void AddItemRandomPlace(int count, int level, ItemType type)
    {
        for (int i = 0; i < count; i++)
        {
            var cells = cellsArray.Cast<Cell>().Where(cell => cell.ItemType == ItemType.Empty);
            if (cells.Count() == 0) { gameOver.Invoke(); }
            else
            {
                var rnd = UnityEngine.Random.Range(0, cells.Count());
                cells.ElementAt(rnd).CreateItem(level, this, type);
            }
        }

    }
   

    public void DiscardTable()
    {
        if (cellsArray != null)
        {
            foreach (var item in cellsArray)
            {
                Destroy(item.gameObject);
            }
        }

    }



}
