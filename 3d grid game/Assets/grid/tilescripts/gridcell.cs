using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gridcell : MonoBehaviour
{
    [SerializeField] protected GameObject highlight;
    protected int posX;
    protected int posY;

    
    //saves a reference to the gameobject that gets placed on this cell
    public GameObject objectinThisGridSpace = null;
    
    
    //saves if the grid space is ocupied or not
    public bool isOcupied = false;
    public bool isWalkable;

    public bool Walkable()
    {
        return !isOcupied && isWalkable;
    }
    
    
    //sets this grid cells position on the grid

    public void setPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }
    
    //get the position of this gridspace on the grid
    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOcupied = true;
       
    }

    private void OnCollisionExit(Collision other)
    {
        isOcupied = false;
    }

    public void setUnit(baseUnit unit)
    {
        if (unit.currentTile != null) {unit.currentTile = null;}
        isOcupied = true;
        unit.currentTile = gameObject;
    }


    private void OnMouseDown()
    {
        //check if it the hero's turn
        if(GameManager.Instance.GameState != GameState.Herotunrn)
        {
            Debug.Log("Not the hero's turn");
                return;
            
        }
        //check if the space is occupied
        if (objectinThisGridSpace != null)
        {
            // Check if the object is part of the hero faction
            Faction factionComponent = objectinThisGridSpace.GetComponent<Faction>();
            if (factionComponent == Faction.Hero)
            {
                Debug.Log("Selected hero unit");
                unitmanager.instance.set_selected_hero(objectinThisGridSpace.GetComponent<Basehero>());
            }
            else if(factionComponent == Faction.Enemy)
            {
                Debug.Log("Selected enemy unit");
                var enemy = objectinThisGridSpace.GetComponent<Baseenemy>();
                Destroy(enemy.GameObject());
                unitmanager.instance.set_selected_hero(null);
            }
        }
        else
        {
            if (unitmanager.instance.selected_hero != null)
            {
                Debug.Log("Grid cell is empty");
                setUnit(unitmanager.instance.selected_hero);
                unitmanager.instance.selected_hero.transform.position=highlight.transform.position;
            }
            
        }
    }
}
