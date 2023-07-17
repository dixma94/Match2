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

    public TableCellManager cellSpawnManager;
    public TaskCellManager taskManager;
    public ItemsCellManager itemsManager;
    public InventoryCellManager inventoryManager;
    public ItemsDragDropHandler itemsDragDropHandler;
    public TableDragDropHandler tableDragDropHandler;
 


   
    
    public static GameState gameState;


    public Action OnStateChanged;

    private GameLevel gameLevel;


    public LevelSystem levelSystem;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        levelSystem = new LevelSystem(cellSpawnManager, taskManager, inventoryManager);
        itemsDragDropHandler.TakeMove += levelSystem.Move;
        tableDragDropHandler.TakeMove += levelSystem.Move;
        cellSpawnManager.gameOver += GameOver;
        InputHandler.Instance.EscKeyDown += PauseResumeGame;
    }
    private void Start()
    {
        StartGame();

    }
    public void ChangeState(GameState mode)
    {
        switch (mode)
        {
            case GameState.Pause:
                gameState = GameState.Pause; 
                break;
            case GameState.Playing:
                gameState = GameState.Playing;
                break;
            case GameState.Over:
                gameState = GameState.Over;
                break;
        }
           OnStateChanged?.Invoke();
    }

    private void PauseResumeGame()
    {
        if (gameState == GameState.Pause)
        {
            ChangeState(GameState.Playing);
            return;
        }
        if (gameState == GameState.Playing)
        {
            ChangeState(GameState.Pause);
            return;
        }
        
    }
    private void ResumeGame()
    {
        
    }

    private void StartGame()
    {
        gameLevel = GameLevel.Level2;


        //создаем поле для игры
        cellSpawnManager.CreateTable();
        for (int i = 0; i < 20; i++)
        {
            cellSpawnManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Obstacle);
        }
        for (int i = 0; i < 5; i++)
        {
            cellSpawnManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Ship);
        }
        for (int i = 0; i < 3; i++)
        {
            cellSpawnManager.GetRandomCell(ItemType.Empty).CreateItem(2, ItemType.Ship);
        }
        cellSpawnManager.GetRandomCell(ItemType.Empty).CreateItem(3, ItemType.Ship);

        //создаем поле для задания

        taskManager.CreateTable();
        taskManager.CreateTask(gameLevel, 6);

        itemsManager.CreateTable();
        itemsManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Ship);

        inventoryManager.CreateTable();
        inventoryManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Rocket);
        ChangeState(GameState.Playing);

        GameSaveLoad.maximumScore = GameSaveLoad.LoadMovesCount();

    }

    public void GameOver()
    {
        ChangeState(GameState.Over);
        
    
    }




}
