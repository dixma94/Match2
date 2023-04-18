using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        cellSpawnManager.AddObstacle(1, 1);
        //создаем поле для задания

        TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
        TaskManager.CreateTask(maxlevel, 5);

        cellSpawnManager.GetComponent<CellChangePosition>().RefreshTask.AddListener(TakeMove);
        
    }

    public void AddShip()
    {

        cellSpawnManager.AddShip(1,1);
    }

   



    public bool FindTask(CellManager cellManager, CellManager taskManager)
    {
        if (taskManager == null) return false;
        List<CellScript> listObstacle = new List<CellScript>();
        List<CellScript> array = new List<CellScript>();

        string task = null;

        int coltask = taskManager.columnsCount;
        int rowtask = taskManager.rowsCount;
        int rowtable = cellManager.rowsCount;

        

        foreach (var item in taskManager.cellsArray)
        {
            task += item.Level.ToString();
        }
        foreach (var item in cellManager.cellsArray)
        {
            array.Add(item);
        }

        for (int i = 0; i < (array.Count - rowtable * (coltask - 1) - (coltask - 1)); i++)
        {
            CellScript[] temp = new CellScript[0];
            listObstacle.Clear();
            string table = null;
            for (int j = 0; j < coltask; j++)
            {
              temp =  temp.Concat(array.Skip((i + rowtable * j)).Take(rowtask)).ToArray();
            
            }
            foreach(var item in temp)
            {
                if (item.CellType == CellType.Obstacle)
                {
                    listObstacle.Add(item);
                    table += "0";
                }
                else
                {
                    table += item.Level.ToString();
                }
            }
            if (task.Equals(table))
            {
                foreach (var item in listObstacle)
                {
                    Destroy(item.ship);
                    item.ship = null;
                    item.Level = 0;
                }
                return true;
            }
        }


       
        return false;
            

    }
    public void TakeMove()
    {
        MovesCounter.CountMoves++;
        if (MovesCounter._CountMoves%8 == 0)
        {
            cellSpawnManager.AddObstacle(1, 1);
        }
        if (FindTask(cellSpawnManager, TaskManager))
        {
            Debug.Log("Задание выполнено");
            TaskManager.DiscardTable();
            TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
            maxlevel++;
            TaskManager.CreateTask(maxlevel, 7);
        }
    }
}
