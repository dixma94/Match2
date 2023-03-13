using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public Vector2 coordinates;
    public GameObject ship;
    public CellManager cellManager;
    public int Level;

    public bool IsHaveShip
    {
        get 
        {
           return ship != null ? true: false;
        }
       
    }
    public int ArrayIndexCol { get; set; }
    public int ArrayIndexRow { get; set; }

    public void CreateShip(int level)
    {
        Level = level;
        ship = Instantiate(cellManager.shipsArray[level-1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
        ship.transform.parent = this.transform;
    }


}
