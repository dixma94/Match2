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
        return firstCell;
    }

    private void DragShip(Vector3 vector)
    {
        Vector3 vector3 = vector;
        vector3.z = -1;
        firstCell.ship.transform.position = vector3;
    }

    private void DownShip(Vector2 vector)
    {
 
            secondCell = _cellManager.FindCell(vector);

        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (secondCell == null || secondCell == firstCell)
        {
            firstCell.ship.transform.position = firstCell.coordinates;
            return;
        }

        //если перетянули на пустую, перемещаем корабль
        if (!secondCell.IsHaveShip)
        {
            firstCell.ship.transform.position = secondCell.coordinates;

            _cellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].ship = firstCell.ship;
            _cellManager.cellsArray[secondCell.ArrayColIndex, secondCell.ArrayRowIndex].Level = firstCell.Level;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].ship = null;
            _cellManager.cellsArray[firstCell.ArrayColIndex, firstCell.ArrayRowIndex].Level = 0;


            RefreshTask.Invoke();
            return;

        }

        //если уровни не совпадают возвращаем
        if (firstCell.Level != secondCell.Level)
        {
            firstCell.ship.transform.position = firstCell.coordinates;
            return;
        }

        //разрущаем корабли и создаем новый
        Destroy(firstCell.ship);
        Destroy(secondCell.ship);
        firstCell.ship = null;

        secondCell.CreateShip(firstCell.Level + 1, _cellManager);
        firstCell.Level = 0;


        RefreshTask.Invoke();
    }

}
