using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CellManager cellSpawnManager;
    public CellManager TaskManager;
    public Button addShipButton;

    private void Start()
    {
        addShipButton.GetComponent<Button>().onClick.AddListener(AddShip);

        //создаем поле для игры
        cellSpawnManager.CreateTable(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        //создаем поле для задания
        
        TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
        TaskManager.AddShips();
        
       
    }

    public void AddShip()
    {

        cellSpawnManager.AddOneShip();
    }

   


    public int Find()
    {
        if (TaskManager != null)
        {
            string task = null;

            int colTasklength = TaskManager.cellsArray.GetUpperBound(0);
            int rowTasklength = TaskManager.cellsArray.GetUpperBound(1);
            int collength = cellSpawnManager.cellsArray.GetUpperBound(0);
            int rowlength = cellSpawnManager.cellsArray.GetUpperBound(1);

            string temp = null;

            foreach (var item in TaskManager.cellsArray)
            {
                task += item.Level.ToString();

            }

            for (int col = 0; col < collength+1; col++)
            {
                if (col > colTasklength) continue;
                for (int row = 0; row < rowlength+1; row++)
                {
                    if (row > rowTasklength) continue;
                    for (int colTask = 0; colTask < colTasklength+ 1; colTask++)
                    {
                        for (int rowTask = 0; rowTask < rowTasklength + 1; rowTask++)
                        {
                            temp += cellSpawnManager.cellsArray[col + colTask, row + rowTask].Level.ToString();
                        }
                    }
                    if (temp == task) return 1;
                    temp = null;
                }
            }

        }
        return -1;

    }

}
