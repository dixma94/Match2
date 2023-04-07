using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CellManager cellSpawnManager;
    public CellManager TaskManager;
    

    private int maxlevel=3;

    private void Start()
    {
       

        //создаем поле для игры
        cellSpawnManager.CreateTable(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        //создаем поле для задания
        
        TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
        TaskManager.CreateTask(maxlevel, 7);

   
    }

    public void AddShip()
    {

        cellSpawnManager.AddShip(1,1);
    }

   



    public bool FindTask(CellManager cellManager, CellManager taskManager)
    {
        if (taskManager == null) return false;

        string task = null;
        string table = null;

        int coltask = taskManager.columnsCount;
        int rowtask = taskManager.rowsCount;
        int rowtable = cellManager.rowsCount;

        foreach (var item in taskManager.cellsArray)
        {
            task += item.Level.ToString();

        }
        foreach (var item in cellManager.cellsArray)
        {
            table += item.Level.ToString();
        }

        for (int i = 0; i < (table.Length - rowtable * (coltask - 1)); i++)
        {
            string temp = null;
            for (int j = 0; j < coltask; j++)
            {
                temp += table.Substring((i + rowtable * j), rowtask);
            }
            if (temp.Equals(task)) return true;


        }
        return false;
            

    }
    public void RefreshTask( CellManager taskManager)
    {
        if (FindTask(cellSpawnManager, taskManager))
            {
                Debug.Log("Задание выполнено");
                taskManager.DiscardTable();
                taskManager.CreateTable(taskManager.columnsCount, taskManager.rowsCount);
                maxlevel++;
                taskManager.CreateTask(maxlevel, 7);
            }
    }
}
