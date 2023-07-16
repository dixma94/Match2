using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TaskCellManager : CellManager
{
    
    public void CreateTask(GameLevel level, int shipsCount)
    {
        if (shipsCount > rowsCount*columnsCount ) return;


        for (int i = 0; i < shipsCount; i++)
        {
            int rndLevel = UnityEngine.Random.Range(1, (int)level + 2);
            GetRandomCell(ItemType.Empty).CreateItem(rndLevel, ItemType.Ship);
        }

    }
    public bool FindTask(TableCellManager cellManager)
    {
        
        List<Cell> listObstacle = new List<Cell>();
        List<Cell> array = new List<Cell>();

        string task = null;

        int rowtable = cellManager.rowsCount;



        foreach (var item in cellsArray)
        {
            task += item.Level.ToString();
        }
        foreach (var item in cellManager.cellsArray)
        {
            array.Add(item);
        }

        for (int i = 0; i < (array.Count - rowtable * (columnsCount - 1) - (columnsCount - 1)); i++)
        {
            Cell[] temp = new Cell[0];
            listObstacle.Clear();
            string table = null;
            for (int j = 0; j < columnsCount; j++)
            {
                temp = temp.Concat(array.Skip((i + rowtable * j)).Take(rowsCount)).ToArray();

            }
            foreach (var item in temp)
            {
                if (item.ItemType == ItemType.Obstacle)
                {
                    listObstacle.Add(item);
                    table += "0";
                }
                else
                {
                    table += item.Level.ToString();
                }
            }
            if (task.Equals(table))
            {
                return true;
            }
        }



        return false;


    }

}
