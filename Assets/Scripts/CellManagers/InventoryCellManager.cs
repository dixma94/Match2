using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCellManager : CellManager
{
    [SerializeField]
    private TableCellManager destinationCellManager;
    private bool firstCLick = true;

    public int points;

    private InputHandler _inputHandler;

    public UnityEvent TakeMove;

    private Cell firstCell;
    private Cell secondCell;
    private void Awake()
    {
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
     void PickUpShip(Vector2 vector)
    {
        if (firstCLick)
        {
            if (FindCell(vector, out firstCell))
            {
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
