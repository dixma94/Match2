using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketDragHandle : DragHandler
{
    [SerializeField]
    private TableCellManager destinationCellManager;
    bool firstCLick = true;

    protected override void OnEnable()
    {
        _inputHandler.ShipUp += PickUpShip;
    }

    protected override void OnDisable()
    {
        _inputHandler.ShipUp -= PickUpShip;
    }
    protected override void PickUpShip(Vector2 vector)
    {
        if (firstCLick)
        {
            if (_cellManager.FindCell(vector, out firstCell))
            {
                firstCell = firstCell.ItemType != ItemType.Rocket ? null : firstCell;
                firstCLick = false;
            }


        }
        else
        {
            if (firstCell != null && destinationCellManager.FindCell(vector, out secondCell) && secondCell.ItemType == ItemType.Obstacle)
            {
                //разрущаем корабли и создаем новый
                Destroy(secondCell.item);
                secondCell.item = null;
                secondCell.ItemType = ItemType.Empty;
                secondCell.Level = 0;
                firstCLick = true;

            }


        }




    }

}
