using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class GameStateSystem 
{

    public GameState gameState;
    public Action OnStateChanged;

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

}
