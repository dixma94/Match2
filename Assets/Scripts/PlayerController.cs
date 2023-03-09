using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CellManager cellSpawnManager;
    private BoxCollider2D collider;

    private CellScript firstTouch;
    private CellScript secondTouch;

    // Start is called before the first frame update
    void Start()
    {
        //Получаем коллайдер и устанавливает его размер равным размеру поля
        collider = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        collider.transform.position = cellSpawnManager.spawnObject.transform.position;
        Vector2 tempVector =cellSpawnManager.GetShiftFromStart(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        collider.size = new Vector2(tempVector.x, -tempVector.y);
    }

    //Меняем позицию корабля при перетаскивании
    private void OnMouseDrag()
    {
        if (firstTouch.IsHaveShip)
        {
            firstTouch.ship.transform.position = GetMousePosition();
        }
    }
    
    private Vector3 GetMousePosition()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;
        return mousePos;
    }

    //Берем клетку на которую нажали
    private void OnMouseDown()
    {
        firstTouch = cellSpawnManager.FindCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
       
    }

    private void OnMouseUp()
    {
        //берем клетку на которую перетянули
        secondTouch = cellSpawnManager.FindCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //проверяем существует ли клетка и корабль
        if (firstTouch ==null)
        {
            return;
        }
        if (!firstTouch.IsHaveShip)
        {
            return;
        }
        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (secondTouch == null)
        {
            firstTouch.ship.transform.position = new Vector3(firstTouch.coordinates.x, firstTouch.coordinates.y, -1);
            return;
        }
        // проверяем есть ли в клетке корабль, совпадают ли уровни и не перятянули ли на саму же себя
        if (!secondTouch.IsHaveShip || firstTouch.Level != secondTouch.Level|| firstTouch == secondTouch)
        {
            firstTouch.ship.transform.position = new Vector3(firstTouch.coordinates.x, firstTouch.coordinates.y, -1);
            return;
        }
        //разрущаем корабли и создаем новый
        Destroy(firstTouch.ship);
        Destroy(secondTouch.ship);
        firstTouch.ship = null;
        secondTouch.ship = null;

        secondTouch.CreateShip(firstTouch.Level + 1);

        

    }
}
