using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DragHandler : MonoBehaviour
{
    protected CellManager _cellManager;
    protected InputHandler _inputHandler;

    public UnityEvent TakeMove;

    protected Cell firstCell;
    protected Cell secondCell;
    protected virtual void Awake()
    {
      _cellManager = GetComponent<CellManager>();
      _inputHandler = GetComponent<InputHandler>();

    }
    protected virtual void OnEnable()
    {
        _inputHandler.ShipUp += PickUpShip;
        _inputHandler.ShipDrag += DragShip;
        _inputHandler.ShipDown += PickDownShip;
    }

    protected virtual void OnDisable()
    {
        _inputHandler.ShipUp -= PickUpShip;
        _inputHandler.ShipDrag -= DragShip;
        _inputHandler.ShipDown -= PickDownShip;
    }
    protected virtual void PickUpShip(Vector2 vector) { }


    protected virtual void DragShip(Vector3 vector) { }


    protected virtual void PickDownShip(Vector2 vector) { }

}


