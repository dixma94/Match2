using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CellManager cellSpawnManager;
    public GameManager gameManager;
    private BoxCollider2D collider;

    private CellScript firstTouch;
    private CellScript secondTouch;

    // Start is called before the first frame update
    void Start()
    {
        //�������� ��������� � ������������� ��� ������ ������ ������� ����
        collider = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        collider.transform.position = cellSpawnManager.spawnObject.transform.position;
        Vector2 tempVector =cellSpawnManager.GetShiftFromStart(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        collider.size = new Vector2(tempVector.x, -tempVector.y);
    }

    //������ ������� ������� ��� ��������������
    private void OnMouseDrag()
    {
        if (firstTouch.IsHaveShip)
        {
            firstTouch.ship.transform.position = GetMousePosition();
        }
    }
    
    private Vector3 GetMousePosition()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -1;
        return mousePos;
    }

    //����� ������ �� ������� ������
    private void OnMouseDown()
    {
        firstTouch = cellSpawnManager.FindCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
       
    }

    private void OnMouseUp()
    {
        //����� ������ �� ������� ����������
        secondTouch = cellSpawnManager.FindCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //��������� ���������� �� ������ � �������
        if (firstTouch ==null)
        {
            return;
        }
        if (!firstTouch.IsHaveShip)
        {
            return;
        }
        //��������� ���������� �� �� ������, ��������� ������� ������ ���� ���
        if (secondTouch == null || secondTouch == firstTouch)
        {
            firstTouch.ship.transform.position = new Vector3(firstTouch.coordinates.x, firstTouch.coordinates.y, -1);
            return;
        }
       
        //���� ���������� �� ������, ���������� �������
        if (!secondTouch.IsHaveShip)
        {
            firstTouch.ship.transform.position = secondTouch.coordinates;

            var StartCell = firstTouch.cellManager.cellsArray[firstTouch.ArrayIndexCol, firstTouch.ArrayIndexRow];
            var FinalCell = secondTouch.cellManager.cellsArray[secondTouch.ArrayIndexCol, secondTouch.ArrayIndexRow];

            FinalCell.ship = firstTouch.ship;
            FinalCell.Level = firstTouch.Level;


            StartCell.ship = null;
            StartCell.Level = 0;

            var s = StartCell.cellManager.cellsArray;
            if (gameManager.Find() != -1)
            {
                Debug.Log("asda");
                gameManager.cellSpawnManager2.DiscardTable();

            }
            return;

        }
        if (firstTouch.Level != secondTouch.Level)
        {
            firstTouch.ship.transform.position = new Vector3(firstTouch.coordinates.x, firstTouch.coordinates.y, -1);
            return;
        }
        //��������� ������� � ������� �����
        Destroy(firstTouch.ship);
        Destroy(secondTouch.ship);
        firstTouch.ship = null;
        secondTouch.ship = null;

        secondTouch.CreateShip(firstTouch.Level + 1);
        firstTouch.Level = 0;


        if (gameManager.Find() != -1)
        {
            Debug.Log("asda");
            gameManager.cellSpawnManager2.DiscardTable();

        }


    }
}
