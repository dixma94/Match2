using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCellManager : CellManager
{
    [SerializeField]
    private TableCellManager destinationCellManager;
    private bool firstCLick = true;
    private Cell firstCell;
    private Cell secondCell;


    public int points;

    public UnityEvent TakeMove;
    public EventHandler<InventoryChangedArgs> InventoryChanged;

    public class InventoryChangedArgs : EventArgs
    {
       public int points;
    }


    private void OnEnable()
    {
        InputHandler.Instance.ShipUp += PickUpShip;
    }

    private void OnDisable()
    {
        InputHandler.Instance.ShipUp -= PickUpShip;

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

            if (points>= 1 
                && firstCell != null 
                && destinationCellManager.FindCell(vector, out secondCell) 
                && secondCell.ItemType == ItemType.Obstacle)
            {
                points-= 1;
                UpdateVisual();
                //разрущаем корабли и создаем новый
                Destroy(secondCell.item);
                secondCell.item = null;
                secondCell.ItemType = ItemType.Empty;
                secondCell.Level = 0;
                firstCLick = true;
            }
        }
    }

    public void UpdateVisual()
    {
        if(points>= 1) 
        { 
            Show(); 
        }
        else 
        { 
            Hide(); 
        }
        InventoryChanged?.Invoke(this, new InventoryChangedArgs() { points= points });
    }

    private void Show()
    {
        foreach (CellInventory item in cellsArray)
        {
            item.Show();
        }
    }
    private void Hide()
    {
        foreach (CellInventory item in cellsArray)
        {
            item.Hide();
        }
    }
}
