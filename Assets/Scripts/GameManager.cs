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
    public TaskCellManager cellTaskManager;
    public ItemsCellManager cellItemsManager;
    public InventoryCellManager CellInventoryManager;
    public ItemsDragDropHandler itemsDragDropHandler;
    public TableDragDropHandler tableDragDropHandler;
    public LevelSystem levelSystem;
    public GameStateSystem stateSystem;
    public GameSaveLoad gameSaveLoad;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        gameSaveLoad = new GameSaveLoad();
        levelSystem = new LevelSystem(cellTableManager, cellTaskManager, CellInventoryManager, gameSaveLoad);
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

        cellTaskManager.CreateTable();
        cellTaskManager.CreateTask(levelSystem.gameLevel, 6);

        cellItemsManager.CreateTable();
        cellItemsManager.CreateItemInCells(1, 1, ItemType.Ship);

        CellInventoryManager.CreateTable();
        CellInventoryManager.CreateItemInCells(1, 1, ItemType.Rocket);

        stateSystem.ChangeState(GameState.Playing);


    }

    private void GameOver()
    {
        stateSystem.ChangeState(GameState.Over);
    }




}
