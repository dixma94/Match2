using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Empty,
    Ship,
    Obstacle
}
public class CellScript : MonoBehaviour
{
    public Vector2 coordinates;
    public GameObject item;
    public int Level;
    public CellType CellType = CellType.Empty;

    

    public bool IsHaveShip
    {
        get 
        {
           return item != null ? true: false;
        }
       
    }
    public int ArrayColIndex { get; set; }
    public int ArrayRowIndex { get; set; }

    public void CreateItem(int level,CellManager cellManager, CellType type)
    {
        Level = level;
        switch (type)
        {
            case CellType.Empty:
                break;
            case CellType.Ship:
                item = Instantiate(cellManager.shipsArray[level - 1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
                break;
            case CellType.Obstacle:
                item = Instantiate(cellManager.obstacleArray[level - 1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
                break;
            default:
                break;
        }
        
        item.transform.parent = this.transform;
        CellType = type;
    }

    
}
