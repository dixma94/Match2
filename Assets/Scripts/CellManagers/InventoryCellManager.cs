using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryCellManager : CellManager
{
    public int points;
    public EventHandler<InventoryChangedArgs> InventoryChanged;
    public class InventoryChangedArgs : EventArgs
    {
       public int points;
    }

    private void Start()
    {
        InventoryChanged?.Invoke(this, new InventoryChangedArgs() { points = points });
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
