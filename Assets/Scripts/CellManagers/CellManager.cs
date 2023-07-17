using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private float cellMargin;
    [SerializeField] private float cellSize = 1;

    public int columnsCount, rowsCount;
    public Cell[,] cellsArray;

    public Action gameOver;

    public void CreateTable()
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
                newCell.arrayColIndex = col;
                newCell.arrayRowIndex = row;
                cellsArray[col, row] = newCell;

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

    public Cell GetRandomCell(ItemType itemType)
    {
        var cells = cellsArray
            .Cast<Cell>()
            .Where(cell => 
            cell.ItemType == ItemType.Empty);
        return  cells
            .ElementAt(UnityEngine.Random.Range(0, cells.Count()));

    }
    public void CreateItemInCells(int cellsCount, int level, ItemType itemType)
    {
        for (int i = 0; i < cellsCount; i++)
        {
            GetRandomCell(ItemType.Empty).CreateItem(level, itemType);
        }
    }

    private Vector2 FindStartPosition(float columnsCount, float rowsCount)
    {
        float startPosX = -(columnsCount * cellSize + (columnsCount - 1) * cellMargin) / 2.0f + cellSize / 2;
        float startPosY = (rowsCount * cellSize + (rowsCount - 1) * cellMargin) / 2.0f - cellSize / 2;
        return new Vector2(startPosX + transform.position.x, startPosY + transform.position.y);
    }

    private Vector2 GetShiftFromStart(int col, int row)
    {
        return new Vector2(col * (cellSize + cellMargin), -row * (cellSize + cellMargin));
    }
   
      
}
