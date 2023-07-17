using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropHandler : MonoBehaviour
{
    public Action TakeMove;
    [SerializeField]
    private protected TableCellManager destinationCellManager;
    [SerializeField]
    private protected GameManager gameManager;

    private protected CellManager cellManager;
    private protected Cell firstCell;
    private protected Cell secondCell;

    private void Start()
    {
        cellManager = GetComponent<CellManager>();
        InputHandler.Instance.ShipUp += PickUpShip;
        InputHandler.Instance.ShipDrag += DragShip;
        InputHandler.Instance.ShipDown += PickDownShip;
    }

    private protected virtual void PickUpShip(Vector2 vector)
    {

    }

    private protected virtual void DragShip(Vector3 vector)
    {

    }

    private protected virtual void PickDownShip(Vector2 vector)
    {

    }
}
