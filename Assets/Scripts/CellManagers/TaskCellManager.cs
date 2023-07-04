using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskCellManager : CellManager
{
    public void CreateTask(int maxLevel, int shipsCount)
    {
        if (shipsCount > cellsArray.Length || maxLevel > shipsArray.Length) return;


        for (int i = 0; i < shipsCount; i++)
        {
            var cells = cellsArray.Cast<Cell>().Where(cell => cell.ItemType == ItemType.Empty);
            var rndElement = UnityEngine.Random.Range(0, cells.Count());
            var rndLevel = UnityEngine.Random.Range(1, maxLevel);
            cells.ElementAt(rndElement).CreateItem(rndLevel, ItemType.Ship);
        }

    }
}
