using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    public CellScript cellPrefab;
    public GameObject spawnObject;
    public CellScript[,] cellsArray;
    public GameObject[] shipsArray;


 

    public int columnsCount, rowsCount;
    public float cellMargin;

    public float cellSize = 1;
   

  
    
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
                newCell.transform.name = col.ToString() + " " + row.ToString();
                newCell.cellManager = this;
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
        return new Vector2(startPosX + spawnObject.transform.position.x, startPosY + spawnObject.transform.position.y);
    }

    public Vector2 FindCellPosition(int xIndex, int yIndex)
    {
        return FindStartPosition(columnsCount, rowsCount) + GetShiftFromStart(xIndex, yIndex);
    }

    public CellScript FindCell(Vector2 point)
    {
        foreach (var cell in cellsArray)
        {
            var halfCellZize = cellSize/2 + cellMargin;
            bool xStatement = cell.coordinates.x - halfCellZize >= point.x || point.x <= cell.coordinates.x + halfCellZize;
            bool yStatement = cell.coordinates.y - halfCellZize <= point.y || point.y >= cell.coordinates.y + halfCellZize;

            if (xStatement && yStatement)
            {
                return cell;
            }
        }
        return null;
    }

    public void CreateShips(int level, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var  cells =  cellsArray.Cast<CellScript>().Where(cell=>!cell.IsHaveShip);
            var rnd =Random.Range(0, cells.Count());
            cells.ElementAt(rnd).CreateShip(level);
        }
    }



   

}
