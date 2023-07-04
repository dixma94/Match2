using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewShipDragHandler : DragHandler
{
    [SerializeField]
    private TableCellManager destinationCellManager;

    protected override void PickUpShip(Vector2 vector)
    {
        if (_cellManager.FindCell(vector, out firstCell))
        {
            firstCell = firstCell.ItemType != ItemType.Ship ? null : firstCell;
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
        if (!destinationCellManager.FindCell(vector, out secondCell) || secondCell == firstCell)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }

        //если перетянули на пустую, перемещаем корабль
        if (!secondCell.IsHaveShip)
        {
            firstCell.item.transform.position = secondCell.coordinates;

            destinationCellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].item = firstCell.item;
            destinationCellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].Level = firstCell.Level;
            destinationCellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].ItemType = firstCell.ItemType;
            firstCell.item = Instantiate(_cellManager.shipsArray[firstCell.Level - 1], new Vector3(firstCell.coordinates.x, firstCell.coordinates.y, -0.5f), gameObject.transform.rotation);
            firstCell.item.transform.parent = this.transform;

            TakeMove?.Invoke();
            return;

        }
        if (firstCell.Level != secondCell.Level)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }


        //разрущаем корабли и создаем новый

        Destroy(secondCell.item);
        secondCell.CreateItem(firstCell.Level + 1, ItemType.Ship);
        firstCell.item.transform.position = firstCell.coordinates;
       

        TakeMove?.Invoke();
    }
}
