using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public Vector2 coordinates;
    public GameObject ship;
    public int Level;

    public bool IsHaveShip
    {
        get 
        {
           return ship != null ? true: false;
        }
       
    }
    public int ArrayColIndex { get; set; }
    public int ArrayRowIndex { get; set; }

    public void CreateShip(int level,CellManager cellManager)
    {
        Level = level;
        ship = Instantiate(cellManager.shipsArray[level-1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
        ship.transform.parent = this.transform;
    }


}
