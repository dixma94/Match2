using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
 

    public static int movesCount;
    public static int maximumScore;
    
    public static GameState gameState;

    public Action MovesCountChanged;
    public Action OnStateChanged;

    private GameLevel gameLevel;
    [SerializeField] private int movesToObstacleMax;
    private int movesToObstacle;

    private const string GAME_PREFS_MAX_SCORE = "MAX SCORE";


    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        itemsDragDropHandler.TakeMove += TakeMove;
        tableDragDropHandler.TakeMove += TakeMove;
        cellSpawnManager.gameOver += GameOver;
        InputHandler.Instance.EscKeyDown += PauseResumeGame;
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
        movesCount = 0;
        MovesCountChanged?.Invoke();

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

        maximumScore = PlayerPrefs.GetInt(GAME_PREFS_MAX_SCORE);

    }

    public void GameOver()
    {
        ChangeState(GameState.Over);
        
    
    }



    public void TakeMove()
    {
        movesCount++;
        movesToObstacle++;
        MovesCountChanged?.Invoke();
        if (movesCount >maximumScore)
        {
            maximumScore = movesCount;
            PlayerPrefs.SetInt(GAME_PREFS_MAX_SCORE, maximumScore);
            PlayerPrefs.Save();
        }

        UpdateLevel();

        if (movesToObstacle == movesToObstacleMax)
        {
            cellSpawnManager.GetRandomCell(ItemType.Empty).CreateItem(1, ItemType.Obstacle);
            movesToObstacle = 0;
        }
        if (taskManager.FindTask(cellSpawnManager))
        {
            
            inventoryManager.points += 1;
            inventoryManager.UpdateVisual();

            taskManager.DiscardTable();
            taskManager.CreateTable();
            taskManager.CreateTask(gameLevel, 6);
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
