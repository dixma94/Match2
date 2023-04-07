using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class CellChangePosition :MonoBehaviour
{
    private CellManager _cellManager;
    private InputHandler _inputHandler;

    CellScript firstCell;
    CellScript secondCell;
    private void Start()
    {
      _cellManager = GetComponent<CellManager>();
      _inputHandler = GetComponent<InputHandler>();
        _inputHandler.ShipUp += PickShip;
        _inputHandler.ShipDrag += DragShip;
        _inputHandler.ShipDown += DownShip;
    }
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        _inputHandler.ShipUp -= PickShip;
        _inputHandler.ShipDrag -= DragShip;
        _inputHandler.ShipDown -= DownShip;
    }
    private CellScript PickShip( Vector2 vector)
    {
        CellScript cell = _cellManager.FindCell(vector);
        return cell;
    }

    private void DragShip(Vector3 vector)
    {
        vector.z = -1;
        firstCell.ship.transform.position = vector;
    }

    private void DownShip(Vector2 vector)
    {
 
            secondCell = _cellManager.FindCell(vector);

        //проверяем перетянули ли на клетку, возращаем позицую первой если нет
        if (secondCell == null || secondCell == firstCell)
        {
            firstCell.ship.transform.position = new Vector3(firstCell.coordinates.x, firstCell.coordinates.y, -1);
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


            //gameManager.RefreshTask();
            return;

        }

        //если уровни не совпадают возвращаем
        if (firstCell.Level != secondCell.Level)
        {
            firstCell.ship.transform.position = new Vector3(firstCell.coordinates.x, firstCell.coordinates.y, -1);
            return;
        }

        //    //разрущаем корабли и создаем новый
        Destroy(firstCell.ship);
        Destroy(secondCell.ship);
        firstCell.ship = null;
        secondCell.ship = null;
        secondCell.CreateShip(firstCell.Level + 1, _cellManager);
        firstCell.Level = 0;


        //    gameManager.RefreshTask();
    }

}
