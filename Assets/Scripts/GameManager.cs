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
        cellTableManager.CreateTable();
        cellTableManager.CreateItemInCells(20, 1, ItemType.Obstacle);
        cellTableManager.CreateItemInCells(5, 1, ItemType.Ship);
        cellTableManager.CreateItemInCells(3, 2, ItemType.Ship);
        cellTableManager.CreateItemInCells(1,3, ItemType.Ship);

        taskManager.CreateTable();
        taskManager.CreateTask(levelSystem.gameLevel, 6);

        itemsManager.CreateTable();
        itemsManager.CreateItemInCells(1, 1, ItemType.Ship);

        inventoryManager.CreateTable();
        inventoryManager.CreateItemInCells(1, 1, ItemType.Rocket);

        stateSystem.ChangeState(GameState.Playing);

        GameSaveLoad.maximumScore = GameSaveLoad.LoadMovesCount();

    }

    private void GameOver()
    {
        stateSystem.ChangeState(GameState.Over);
    }




}
