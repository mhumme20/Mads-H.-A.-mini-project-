using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState GameState;
    
    public grid_script grid;
    public unitmanager units;

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.GenerateGrid :
                grid.CreateGrid();
                break;
            case GameState.SpawnHero:
                units.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                units.SpawnVillians();
                break;
            case GameState.Herotunrn:
                break;
            case GameState.EnemiesTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    
    
    
}

public enum GameState
{
    GenerateGrid=0,
    SpawnHero=1,
    SpawnEnemies=2,
    Herotunrn=3,
    EnemiesTurn=4
}
