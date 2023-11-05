using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class grid_script : MonoBehaviour
{
  public static grid_script instance;

  private int lenght = 20;
  private int width = 20;
  private float gridspacesize = 1f;

  [SerializeField] private GameObject floortile,funituretile;
  private GameObject[,] gameGrid;


  
  public void CreateGrid()
  {
    gameGrid = new GameObject[lenght, width];
    
    

    for (int y = 0; y < lenght; y++)
    {
      for (int x = 0; x < width; x++)
      {
        //create new gridspace obj for each cell
        
        var gridCellPrefab = Random.Range(0, 6) == 3 ? funituretile : floortile;
        gameGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x*gridspacesize,transform.position.y,y*gridspacesize),Quaternion.identity);
        gameGrid[x,y].GetComponent<gridcell>().setPosition(x,y);
        gameGrid[x, y].transform.parent = transform;
        gameGrid[x, y].gameObject.name = "grid space(x" + x.ToString() + ", Y" + y.ToString() + ")";
      }
    }
    GameManager.Instance.ChangeState(GameState.SpawnHero);
  }
  
  //Gets the grid position from the world position
  public Vector2Int GetGridPosiFromWorld(Vector3 worldPosition)
  {
    int x = Mathf.FloorToInt(worldPosition.x / gridspacesize);
    int y = Mathf.FloorToInt(worldPosition.z / gridspacesize);
    x = Mathf.Clamp(x, 0, width);
    y = Mathf.Clamp(y, 0, lenght);

    return new Vector2Int(x, y);
  }

  public GameObject GetRandomHerospawnTile()
  {
    List<GameObject> heroSpawnTiles = new List<GameObject>();
    int maxY = Mathf.FloorToInt(lenght/4); 

    for (int y = 0; y < maxY; y++)
    {
      for (int x = 0; x < width; x++)
      {
        //creates a list of eligible tiles
        GameObject tile = gameGrid[x, y];
        
        //check if the tile is unocupied and walkable
        gridcell gridCell = tile.GetComponent<gridcell>();
        if (gridCell.Walkable())
        {
            heroSpawnTiles.Add(tile); 
        }
        
      }
    }

    if (heroSpawnTiles.Count > 0)//checks if the are any tiles in the list
    {
      int randomIndex = Random.Range(0, heroSpawnTiles.Count);
      return heroSpawnTiles[randomIndex]; //returns a random tile from the list
    }
    else
    {
      return null; // Return null if there are no eligible tiles.
    }
  }
  
  public GameObject GetRandomEnemypawnTile()
  {
    List<GameObject> EnemySpawnTiles = new List<GameObject>();
    int minY = Mathf.FloorToInt((lenght/4)*3); 

    for (int y = minY; y < lenght; y++)
    {
      for (int x = 0; x < width; x++)
      {
        //creates a list of eligible tiles
        GameObject tile = gameGrid[x, y];
        
        //check if the tile is unocupied and walkable
        gridcell gridCell = tile.GetComponent<gridcell>();
        if (gridCell.Walkable())
        {
          EnemySpawnTiles.Add(tile); 
        }
        
      }
    }

    if (EnemySpawnTiles.Count > 0)//checks if the are any tiles in the list
    {
      int randomIndex = Random.Range(0, EnemySpawnTiles.Count);
      return EnemySpawnTiles[randomIndex]; //returns a random tile from the list
    }
    else
    {
      return null; // Return null if there are no eligible tiles.
    }
  }
  
  
  //Get the world position of a grid position
  public Vector3 GetWorldPosiFromGridPos(Vector2Int gridPos)
  {
    float x = gridPos.x * gridspacesize;
    float y = gridPos.y * gridspacesize;

    return new Vector3(x,0, y);
  }
}
