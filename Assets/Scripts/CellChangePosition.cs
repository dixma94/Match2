using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class CellChangePosition :MonoBehaviour
{
    private CellManager _cellManager;
    private InputHandler _inputHandler;

    public UnityEvent RefreshTask;

    CellScript firstCell;
    CellScript secondCell;
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
    private void PickUpShip( Vector2 vector)
    {
        if (_cellManager.FindCell(vector, out firstCell))
        {
             firstCell= firstCell.CellType != CellType.Ship ? null : firstCell;
        }

    }

    private void DragShip(Vector3 vector)
    {
        firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
    }

    private void PickDownShip(Vector2 vector)
    {

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
            _cellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].CellType = firstCell.CellType;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].item = null;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].Level = 0;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].CellType = CellType.Empty;


            RefreshTask?.Invoke();
            return;

        }

        //если уровни не совпадают возвращаем
        if (firstCell.Level != secondCell.Level)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }

        //разрущаем корабли и создаем новый
        Destroy(firstCell.item);
        Destroy(secondCell.item);
        firstCell.item = null;

        secondCell.CreateItem(firstCell.Level + 1, _cellManager, CellType.Ship);
        firstCell.Level = 0;


        RefreshTask?.Invoke();
    }

}
