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
        _inputHandler.ShipUp += PickShip;
        _inputHandler.ShipDrag += DragShip;
        _inputHandler.ShipDown += DownShip;
    }

    private void OnDisable()
    {
        _inputHandler.ShipUp -= PickShip;
        _inputHandler.ShipDrag -= DragShip;
        _inputHandler.ShipDown -= DownShip;
    }
    private CellScript PickShip( Vector2 vector)
    {
        firstCell = _cellManager.FindCell(vector);
        if (firstCell != null) return firstCell.CellType != CellType.Ship ? null : firstCell;
        return null;


    }

    private void DragShip(Vector3 vector)
    {
        Vector3 vector3 = vector;
        vector3.z = -1;
        firstCell.item.transform.position = vector3;
    }

    private void DownShip(Vector2 vector)
    {
 
            secondCell = _cellManager.FindCell(vector);

        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (secondCell == null || secondCell == firstCell)
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


            RefreshTask.Invoke();
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


        RefreshTask.Invoke();
    }

}
