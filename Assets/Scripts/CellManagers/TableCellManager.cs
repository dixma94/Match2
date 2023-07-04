using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class TableCellManager : CellManager
{


    public UnityEvent TakeMove;

    private Cell firstCell;
    private Cell secondCell;


    private void OnEnable()
    {
        InputHandler.Instance.ShipUp += PickUpShip;
        InputHandler.Instance.ShipDrag += DragShip;
        InputHandler.Instance.ShipDown += PickDownShip;
    }

    private void OnDisable()
    {
        InputHandler.Instance.ShipUp -= PickUpShip;
        InputHandler.Instance.ShipDrag -= DragShip;
        InputHandler.Instance.ShipDown -= PickDownShip;
    }

    private void PickUpShip(Vector2 vector)
    {
        if (FindCell(vector, out firstCell))
        {
            firstCell = firstCell.ItemType != ItemType.Ship ? null : firstCell;
        }

    }

    private void DragShip(Vector3 vector)
    {
        if (firstCell != null)
        {

            firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
        }
    }

    private void PickDownShip(Vector2 vector)
    {
        if (firstCell == null) return;

        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (!FindCell(vector, out secondCell) || secondCell == firstCell)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }

        //если перетянули на пустую, перемещаем корабль
        if (secondCell.ItemType == ItemType.Empty)
        {
            firstCell.item.transform.position = secondCell.coordinates;

            secondCell.item = firstCell.item;
            secondCell.Level = firstCell.Level;
            secondCell.ItemType = firstCell.ItemType;
            firstCell.item = null;
            firstCell.Level = 0;
            firstCell.ItemType = ItemType.Empty;

            TakeMove?.Invoke();
            return;

        }
        if (firstCell.Level != secondCell.Level)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }


        //разрущаем корабли и создаем новый
        Destroy(firstCell.item);
        Destroy(secondCell.item);
        firstCell.item = null;
        firstCell.ItemType = ItemType.Empty;
        secondCell.CreateItem(firstCell.Level + 1, ItemType.Ship);
        firstCell.Level = 0;


        TakeMove?.Invoke();
    }

}
