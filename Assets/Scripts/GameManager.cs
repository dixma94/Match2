using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CellManager cellSpawnManager;
    public CellManager cellSpawnManager2;
    public Button addShipButton;

    private void Start()
    {
        addShipButton.GetComponent<Button>().onClick.AddListener(AddShip);

        //������� ���� ��� ����
        cellSpawnManager.CreateTable(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        
        //������� ���� ��� �������
        cellSpawnManager2.CreateTable(cellSpawnManager2.columnsCount, cellSpawnManager2.rowsCount);
        cellSpawnManager2.CreateShips();
    }

    public void AddShip()
    {
        cellSpawnManager.CreateOneShip();
    }


}
