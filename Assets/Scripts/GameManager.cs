using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TableCellManager cellSpawnManager;
    public TaskCellManager TaskManager;
    public ItemsCellManager ItemsManager;
    public InventoryCellManager InventoryManager;
    public GameObject Canvas;
    public int point;
    

    private int maxlevel=3;

    private void Start()
    {
       

        //создаем поле для игры
        cellSpawnManager.CreateTable(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        cellSpawnManager.AddItemRandomPlace(15, 1,ItemType.Obstacle);
        cellSpawnManager.AddItemRandomPlace(5, 1,ItemType.Ship);
        cellSpawnManager.AddItemRandomPlace(3, 2, ItemType.Ship);
        cellSpawnManager.AddItemRandomPlace(1, 3, ItemType.Ship);
        //создаем поле для задания

        TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
        TaskManager.CreateTask(maxlevel, 5);

        ItemsManager.CreateTable(ItemsManager.columnsCount, ItemsManager.rowsCount);
        ItemsManager.AddItemRandomPlace(1, 1, ItemType.Ship);
        ItemsManager.GetComponent<NewShipDragHandler>().TakeMove.AddListener(TakeMove);

        InventoryManager.CreateTable(InventoryManager.columnsCount, InventoryManager.rowsCount);
        InventoryManager.AddItemRandomPlace(1, 1, ItemType.Rocket);

        cellSpawnManager.GetComponent<ShipDragHandler>().TakeMove.AddListener(TakeMove);
        cellSpawnManager.gameOver += GameOver;
    }


   
    public void GameOver()
    {
        Canvas.SetActive(true);
    }


    public bool FindTask(TableCellManager cellManager, TaskCellManager taskManager)
    {
        if (taskManager == null) return false;
        List<Cell> listObstacle = new List<Cell>();
        List<Cell> array = new List<Cell>();

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
            Cell[] temp = new Cell[0];
            listObstacle.Clear();
            string table = null;
            for (int j = 0; j < coltask; j++)
            {
              temp =  temp.Concat(array.Skip((i + rowtable * j)).Take(rowtask)).ToArray();
            
            }
            foreach(var item in temp)
            {
                if (item.ItemType == ItemType.Obstacle)
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
                //foreach (var item in listObstacle)
                //{
                //    Destroy(item.item);
                //    item.item = null;
                //    item.Level = 0;
                //}
                return true;
            }
        }


       
        return false;
            

    }
    public void TakeMove()
    {
        MovesCounter.CountMoves++;
        if (MovesCounter.CountMoves%20 == 0 && maxlevel < cellSpawnManager.shipsArray.Length)
        {
            maxlevel++;
        }
        if (MovesCounter._CountMoves%8 == 0)
        {
            cellSpawnManager.AddItemRandomPlace(1, 1, ItemType.Obstacle);
        }
        if (FindTask(cellSpawnManager, TaskManager))
        {
            point += 5;
            RefreshTask();
        }
    }

    public void RefreshTask()
    {
        TaskManager.DiscardTable();
        TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
        TaskManager.CreateTask(maxlevel, 5);
    }
}
