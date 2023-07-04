using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DragHandler : MonoBehaviour
{
    protected CellManager _cellManager;


    public UnityEvent TakeMove;

    protected Cell firstCell;
    protected Cell secondCell;
    protected virtual void Awake()
    {
      _cellManager = GetComponent<CellManager>();


    }
    protected virtual void OnEnable()
    {
        InputHandler.Instance.ShipUp += PickUpShip;
        InputHandler.Instance.ShipDrag += DragShip;
        InputHandler.Instance.ShipDown += PickDownShip;
    }

    protected virtual void OnDisable()
    {
        InputHandler.Instance.ShipUp -= PickUpShip;
        InputHandler.Instance.ShipDrag -= DragShip;
        InputHandler.Instance.ShipDown -= PickDownShip;
    }
    protected virtual void PickUpShip(Vector2 vector) { }


    protected virtual void DragShip(Vector3 vector) { }


    protected virtual void PickDownShip(Vector2 vector) { }

}


