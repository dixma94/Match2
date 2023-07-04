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



    public void TakeMove()
    {
        MovesCounter.CountMoves++;
        if (MovesCounter.CountMoves%20 == 0)
        {
            maxlevel++;
        }
        if (MovesCounter._CountMoves%8 == 0)
        {
            cellSpawnManager.AddItemRandomPlace(1, 1, ItemType.Obstacle);
        }
        if (TaskManager.FindTask(cellSpawnManager))
        {
            point += 5;
            TaskManager.DiscardTable();
            TaskManager.CreateTable(TaskManager.columnsCount, TaskManager.rowsCount);
            TaskManager.CreateTask(maxlevel, 4);
        }
    }


}
