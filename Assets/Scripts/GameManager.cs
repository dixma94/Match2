using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public enum GameState
{
    Pause,
    Playing,
    Over
}
public class GameManager : MonoBehaviour
{

    public TableCellManager cellTableManager;
    public TaskCellManager taskManager;
    public ItemsCellManager itemsManager;
    public InventoryCellManager inventoryManager;
    public ItemsDragDropHandler itemsDragDropHandler;
    public TableDragDropHandler tableDragDropHandler;
    public LevelSystem levelSystem;
    public GameStateSystem stateSystem;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        levelSystem = new LevelSystem(cellTableManager, taskManager, inventoryManager);
        stateSystem = new GameStateSystem();
        itemsDragDropHandler.TakeMove += levelSystem.Move;
        tableDragDropHandler.TakeMove += levelSystem.Move;
        cellTableManager.gameOver += GameOver;
        InputHandler.Instance.EscKeyDown += PauseResumeGame;
    }
    private void Start()
    {
        StartGame();

    }

    private void PauseResumeGame()
    {
        if (stateSystem.gameState == GameState.Pause)
        {
            stateSystem.ChangeState(GameState.Playing);
            return;
        }
        if (stateSystem.gameState == GameState.Playing)
        {
            stateSystem.ChangeState(GameState.Pause);
            return;
        }
        
    }

    private void StartGame()
    {
        //создаем поле для игры
        cellTableManager.CreateTable();
        for (int i = 0; i < 20; i++)
        {
            cellTableManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Obstacle);
        }
        for (int i = 0; i < 5; i++)
        {
            cellTableManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Ship);
        }
        for (int i = 0; i < 3; i++)
        {
            cellTableManager.GetRandomCell(ItemType.Empty).CreateItem(2, ItemType.Ship);
        }
        cellTableManager.GetRandomCell(ItemType.Empty).CreateItem(3, ItemType.Ship);

        //создаем поле для задания

        taskManager.CreateTable();
        taskManager.CreateTask(levelSystem.gameLevel, 6);

        itemsManager.CreateTable();
        itemsManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Ship);

        inventoryManager.CreateTable();
        inventoryManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Rocket);
        stateSystem.ChangeState(GameState.Playing);

        GameSaveLoad.maximumScore = GameSaveLoad.LoadMovesCount();

    }

    private void GameOver()
    {
        stateSystem.ChangeState(GameState.Over);
    }




}
