using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDragDropHandler : DragDropHandler
{
    private bool firstCLick = true;

    override private protected void PickUpShip(Vector2 vector)
    {
        if (gameManager.stateSystem.gameState == GameState.Playing)
        {


            if (firstCLick)
            {
                if (cellManager.FindCell(vector, out firstCell))
                {
                    firstCLick = false;
                }
            }
            else
            {
                InventoryCellManager inventoryCellManager = cellManager as InventoryCellManager;

                if (inventoryCellManager.points >= 1
                    && firstCell != null
                    && destinationCellManager.FindCell(vector, out secondCell)
                    && secondCell.ItemType == ItemType.Obstacle)
                {
                    inventoryCellManager.points -= 1;
                    inventoryCellManager.UpdateVisual();
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
}
