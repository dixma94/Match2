using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipDragHandler : MonoBehaviour
{
    private CellManager _cellManager;
    public CellManager destinationCellManager;
    private InputHandler _inputHandler;

    public UnityEvent TakeMove;

    Cell firstCell;
    Cell secondCell;
    private void Awake()
    {
        _cellManager = GetComponent<CellManager>();
        _inputHandler = GetComponent<InputHandler>();

    }
    private void OnEnable()
    {
        _inputHandler.ShipUp += PickUpShip;
        _inputHandler.ShipDrag += DragShip;
        _inputHandler.ShipDown += PickDownShip;
    }

    private void OnDisable()
    {
        _inputHandler.ShipUp -= PickUpShip;
        _inputHandler.ShipDrag -= DragShip;
        _inputHandler.ShipDown -= PickDownShip;
    }
    private void PickUpShip(Vector2 vector)
    {
        if (_cellManager.FindCell(vector, out firstCell))
        {
            firstCell = firstCell.CellType != CellType.Ship ? null : firstCell;
        }

    }

    private void DragShip(Vector3 vector)
    {
        firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
    }

    private void PickDownShip(Vector2 vector)
    {

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
            destinationCellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].CellType = firstCell.CellType;
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
        secondCell.CreateItem(firstCell.Level + 1, destinationCellManager, CellType.Ship);
        firstCell.item.transform.position = firstCell.coordinates;
       

        TakeMove?.Invoke();
    }
}
