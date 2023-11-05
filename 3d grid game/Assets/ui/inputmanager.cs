using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputmanager : MonoBehaviour
{
     grid_script GameGrid;
     [SerializeField] private LayerMask whatIsAGridLayer;
     
     
    // Start is called before the first frame update
    void Start()
    {
        GameGrid = FindObjectOfType<grid_script>();

    }

    // Update is called once per frame
    void Update()
    {
        gridcell cellMouseIsOver = IsMouseOverAGridSpace();
        if (cellMouseIsOver != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cellMouseIsOver.GetComponentInChildren<SpriteRenderer>().color=Color.green;
                Debug.Log(cellMouseIsOver.isOcupied);
            }
        }
    }
    
    //Returns the grid cell if mouse is over a grid cell and return null if it is not
    private gridcell IsMouseOverAGridSpace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, whatIsAGridLayer))
        {
            return hitInfo.transform.GetComponent<gridcell>();
        }
        else
        {
            return null;  
        }
        
    }
    
}
