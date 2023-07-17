using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell : MonoBehaviour
{
    public Vector2 coordinates;
    public GameObject item;
    public ItemType ItemType = ItemType.Empty;
    public GameObject[] shipsArray;
    public GameObject[] obstacleArray;
    public GameObject[] rocketsArray;
    public int Level;
    public int arrayColIndex;
    public int arrayRowIndex;


    public void CreateItem(int level,ItemType type)
    {
        Vector3 _cordinates = new Vector3(coordinates.x, coordinates.y, -0.5f);
        switch (type)
        {
            case ItemType.Empty:
                break;
            case ItemType.Ship:
                Level = level;
                item = Instantiate(shipsArray[level - 1], _cordinates, gameObject.transform.rotation);
                break;
            case ItemType.Rocket:
                Level = level;
                item = Instantiate(rocketsArray[level - 1], _cordinates, gameObject.transform.rotation);
                break;
            case ItemType.Obstacle:
                item = Instantiate(obstacleArray[level - 1], _cordinates, gameObject.transform.rotation);
                Level = -1;
                break;

            default:
                break;
        }

        item.transform.parent = this.transform;
        ItemType = type;
    }


}
