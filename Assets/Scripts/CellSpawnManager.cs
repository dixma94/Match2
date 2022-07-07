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

                var shiftVector = new Vector3(startPos.x + i*(cellMargin+1), startPos.y - j * (cellMargin + 1), 0);

                var newCell = Instantiate(cellPrefab, shiftVector, cellPrefab.transform.rotation);

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

    public Vector3 FindCellPosFromIndex(int xIndex, int yIndex)
    {
        return cellsArray[xIndex, yIndex].transform.position;
    }

}
