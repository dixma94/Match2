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

    private void OnEnable()
    {
        InputHandler.Instance.ShipUp += PickUpShip;
        InputHandler.Instance.ShipDrag += DragShip;
        InputHandler.Instance.ShipDown += PickDownShip;
    }

    private void OnDisable()
    {
        InputHandler.Instance.ShipUp -= PickUpShip;
        InputHandler.Instance.ShipDrag -= DragShip;
        InputHandler.Instance.ShipDown -= PickDownShip;
    }
    private void PickUpShip(Vector2 vector)
    {
        if (GameManager.gameState == GameState.Playing)
        {
            if (FindCell(vector, out firstCell))
            {
                firstCell = firstCell.ItemType != ItemType.Ship ? null : firstCell;
            }
        }
       

    }

    private void DragShip(Vector3 vector)
    {
        if (firstCell != null)
        {

            firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
        }
    }

    private void PickDownShip(Vector2 vector)
    {
        if (firstCell == null) return;
        //��������� ���������� �� �� ������, ��������� ������� ������ ���� ���
        if (!destinationCellManager.FindCell(vector, out secondCell) || secondCell == firstCell)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }

        //���� ���������� �� ������, ���������� �������
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


        //��������� ������� � ������� �����

        Destroy(secondCell.item);
        secondCell.CreateItem(firstCell.Level + 1, ItemType.Ship);
        firstCell.item.transform.position = firstCell.coordinates;


        TakeMove?.Invoke();
    }
}
