using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketDragHandle : MonoBehaviour
{
    private CellManager _cellManager;
    public CellManager destinationCellManager;
    private InputHandler _inputHandler;

    Cell firstCell;
    Cell secondCell;

    bool firstCLick = true;

    private void Awake()
    {
        _cellManager = GetComponent<CellManager>();
        _inputHandler = GetComponent<InputHandler>();

    }
    private void OnEnable()
    {
        _inputHandler.ShipUp += PickUpShip;
    }

    private void OnDisable()
    {
        _inputHandler.ShipUp -= PickUpShip;
    }
    private void PickUpShip(Vector2 vector)
    {
        if (firstCLick)
        {
            if (_cellManager.FindCell(vector, out firstCell))
            {
                firstCell = firstCell.CellType != CellType.Rocket ? null : firstCell;
                firstCLick = false;
            }


        }
        else
        {
            if (firstCell == null) return;
            //проверяем перетянули ли на клетку, возращаем позицую первой если нет
            if (!destinationCellManager.FindCell(vector, out secondCell) || secondCell == firstCell)
            {
                firstCell.item.transform.position = firstCell.coordinates;
                return;
            }

            if (secondCell.CellType == CellType.Obstacle)
            {
                //разрущаем корабли и создаем новый
                Destroy(secondCell.item);
                secondCell.item = null;
                secondCell.CellType = CellType.Empty;

            }
            firstCell.item.transform.position = firstCell.coordinates;
            firstCLick = true;

        }




    }

}
