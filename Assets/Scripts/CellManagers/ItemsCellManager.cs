using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemsCellManager : CellManager
{
    public UnityEvent TakeMove;
    [SerializeField]
    private TableCellManager destinationCellManager;

    private Cell firstCell;
    private Cell secondCell;


    override private protected void PickUpShip(Vector2 vector)
    {
        if (GameManager.gameState == GameState.Playing)
        {
            if (FindCell(vector, out firstCell))
            {
                firstCell = firstCell.ItemType != ItemType.Ship ? null : firstCell;
            }
        }

    }

    override private protected void DragShip(Vector3 vector)
    {
        if (firstCell != null)
        {

            firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
        }
    }

    override private protected void PickDownShip(Vector2 vector)
    {
        if (firstCell == null) return;
        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (!destinationCellManager.FindCell(vector, out secondCell) || secondCell == firstCell)
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
            firstCell.CreateItem(firstCell.Level, firstCell.ItemType);


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
