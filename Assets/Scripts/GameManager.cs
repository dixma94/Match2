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

        //создаем поле для игры
        cellSpawnManager.CreateTable(cellSpawnManager.columnsCount, cellSpawnManager.rowsCount);
        
       
    }

    public void AddShip()
    {
        //создаем поле для задания
        cellSpawnManager2.DiscardTable();
        cellSpawnManager2.CreateTable(cellSpawnManager2.columnsCount, cellSpawnManager2.rowsCount);
        cellSpawnManager2.CreateShips();

        Find();
    }



    public void Find()
    {
        if (cellSpawnManager2 != null)
        {
            string task = null ;
            string levels = null ;
            foreach (var item in cellSpawnManager2.cellsArray)
            {
                task += item.Level.ToString();
                    
            }

            foreach (var item in cellSpawnManager.cellsArray )
            {
                levels += item.Level.ToString(); 
            }

            int i = levels.IndexOf(task);
            Debug.Log(i.ToString());
            
        }
    }

}
