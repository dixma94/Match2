using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class ShipDragHandler : DragHandler
{

    protected override void PickUpShip( Vector2 vector)
    {
        if (_cellManager.FindCell(vector, out firstCell))
        {
             firstCell= firstCell.ItemType != ItemType.Ship ? null : firstCell;
        }

    }

    protected override void DragShip(Vector3 vector)
    {
        if (firstCell != null)
        {

        firstCell.item.transform.position = new Vector3(vector.x, vector.y, -1);
        }
    }

    protected override void PickDownShip(Vector2 vector)
    {
        if (firstCell == null) return;

        //��������� ���������� �� �� ������, ��������� ������� ������ ���� ���
        if (!_cellManager.FindCell(vector,out secondCell) || secondCell == firstCell)
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
            firstCell.item = null;
            firstCell.Level = 0;
            firstCell.ItemType = ItemType.Empty;

            TakeMove?.Invoke();
            return;

        }
        if (firstCell.Level != secondCell.Level)
        {
            firstCell.item.transform.position = firstCell.coordinates;
            return;
        }
        

        //��������� ������� � ������� �����
        Destroy(firstCell.item);
        Destroy(secondCell.item);
        firstCell.item = null;
        firstCell.ItemType = ItemType.Empty;
        secondCell.CreateItem(firstCell.Level + 1, ItemType.Ship);
        firstCell.Level = 0;


        TakeMove?.Invoke();
    }

}
