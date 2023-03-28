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

    private int maxlevel=2;

    private void Start()
    {
        addShipButton.GetComponent<Button>().onClick.AddListener(AddShip);

        //создаем поле для игры
        cellSpawnManager.CreateTable(7, 7);
        //создаем поле для задания
        
        TaskManager.CreateTable(3, 3);
        TaskManager.CreateTask(maxlevel, 7);
        
       
    }

    public void AddShip()
    {

        cellSpawnManager.AddShip(1,1);
    }




    public bool FindTask()
    {
        if (TaskManager == null) return false;

        string task = null;
        string table = null;

        int coltask = TaskManager.ColumnsCount;
        int rowtask = TaskManager.RowsCount;
        int rowtable = cellSpawnManager.RowsCount;

        foreach (var item in TaskManager.cellsArray)
        {
            task += item.Level.ToString();

        }
        foreach (var item in cellSpawnManager.cellsArray)
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
    public void RefreshTask()
    {
        if (FindTask())
            {
                Debug.Log("Задание выполнено");
                TaskManager.DiscardTable();
                TaskManager.CreateTable(3, 3);
                maxlevel++;
                TaskManager.CreateTask(maxlevel, 7);
            }
    }
}
