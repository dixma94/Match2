using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Empty,
    Ship,
    Obstacle,
    Rocket
}
public class Cell : MonoBehaviour
{
    public Vector2 coordinates;
    public GameObject item;
    public ItemType ItemType = ItemType.Empty;
    public GameObject[] shipsArray;
    public GameObject[] obstacleArray;
    public GameObject[] rocketsArray;
    public int Level;

    public bool IsHaveShip
    {
        get 
        {
           return item != null ? true: false;
        }
       
    }
    public int ArrayColIndex { get; set; }
    public int ArrayRowIndex { get; set; }

    public void CreateItem(int level, ItemType type)
    {

        switch (type)
        {
            case ItemType.Empty:
                break;
            case ItemType.Ship:
                Level = level;
                item = Instantiate(shipsArray[level -1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
                break;
            case ItemType.Rocket:
                Level = level;
                item = Instantiate(rocketsArray[level-1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
                break;
            case ItemType.Obstacle:
                item = Instantiate(obstacleArray[level-1], new Vector3(coordinates.x, coordinates.y, -0.5f), gameObject.transform.rotation);
                Level = -1;
                break;

            default:
                break;
        }
        
        item.transform.parent = this.transform;
        ItemType = type;
    }


}
