using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLevel
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8

}

public class LevelSystem 
{
    public Action MovesCountChanged;
    public int movesCount;
    private int movesToObstacleMax;
    private int movesToObstacle;
    private TableCellManager tableCellManager;
    private TaskCellManager taskCellManager;
    private InventoryCellManager inventoryCellManager;
    private GameLevel gameLevel;


    public LevelSystem(TableCellManager tableCellManager, TaskCellManager taskCellManager, InventoryCellManager inventoryCellManager)
    {
        movesCount = 0;
        movesToObstacle = 0;
        movesToObstacleMax = 8;
        this.tableCellManager = tableCellManager;
        this.taskCellManager = taskCellManager;
        this.inventoryCellManager = inventoryCellManager;
        MovesCountChanged?.Invoke();
    }


    public void Move()
    {
        movesCount++;
        movesToObstacle++;
        MovesCountChanged?.Invoke();

        if (movesCount > GameSaveLoad.maximumScore)
            GameSaveLoad.Save(movesCount);

        UpdateLevel();

        if (movesToObstacle == movesToObstacleMax)
        {
            tableCellManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Obstacle);
            movesToObstacle = 0;
        }
        if (taskCellManager.FindTask(tableCellManager))
        {

            inventoryCellManager.points += 1;
            inventoryCellManager.UpdateVisual();

            taskCellManager.DiscardTable();
            taskCellManager.CreateTable();
            taskCellManager.CreateTask(gameLevel, 6);
        }

    }

    private void UpdateLevel()
    {
        if (movesCount < 20) { gameLevel = GameLevel.Level2; return; }
        if (movesCount < 50) { gameLevel = GameLevel.Level3; return; }
        if (movesCount < 100) { gameLevel = GameLevel.Level4; return; }
        if (movesCount < 200) { gameLevel = GameLevel.Level5; return; }
        if (movesCount < 300) { gameLevel = GameLevel.Level6; return; }
        if (movesCount < 400) { gameLevel = GameLevel.Level7; return; }
        if (movesCount < int.MaxValue) { gameLevel = GameLevel.Level8; }

    }
}
