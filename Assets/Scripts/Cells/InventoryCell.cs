using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : Cell
{
    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject back;
    public void Show()
    {
        cell.SetActive(true);
        item.SetActive(true);
        back.SetActive(false);
    }
    public void Hide()
    {
        cell.SetActive(false);
        item.SetActive(false);
        back.SetActive(true);
    }
}
