using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class ShipDragHandler : DragHandler
{

    protected override void PickUpShip( Vector2 vector)
    {
        if (_cellManager.FindCell(vector, out firstCell))
        {
             firstCell= firstCell.ItemType != ItemType.Ship ? null : firstCell;
        }

    }

    protected override void DragShip(Vector3 vector)
    {
        if (firstCell != null)
        {

        firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
        }
    }

    protected override void PickDownShip(Vector2 vector)
    {
        if (firstCell == null) return;

        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (!_cellManager.FindCell(vector,out secondCell) || secondCell == firstCell)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }

        //если перетянули на пустую, перемещаем корабль
        if (!secondCell.IsHaveShip)
        {
            firstCell.item.transform.position = secondCell.coordinates;

            _cellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].item = firstCell.item;
            _cellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].Level = firstCell.Level;
            _cellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].ItemType = firstCell.ItemType;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].item = null;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].Level = 0;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].ItemType = ItemType.Empty;


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
        secondCell.CreateItem(firstCell.Level + 1, _cellManager, ItemType.Ship);
        firstCell.Level = 0;


        TakeMove?.Invoke();
    }

}
