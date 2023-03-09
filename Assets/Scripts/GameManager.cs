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

        cellSpawnManager.CreateTable(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        cellSpawnManager.CreateShips(0,2);

        cellSpawnManager2.CreateTable(cellSpawnManager2.columnsCount, cellSpawnManager2.rowsCount);
        cellSpawnManager2.CreateShips(0, 2);
    }

    public void AddShip()
    {
        cellSpawnManager.CreateShips(0,1);
    }


}
