using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static InventoryCellManager;

public class InventoryCountUI : MonoBehaviour
{
    [SerializeField] private InventoryCellManager cellManager;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private void Start()
    {
        cellManager.InventoryChanged += CellManager_InventoryChanged;
    }

    private void CellManager_InventoryChanged(object sender, InventoryChangedArgs e )
    {
        textMeshProUGUI.text = e.points.ToString();
        if (e.points>0)
        {
            textMeshProUGUI.gameObject.SetActive(true);
        }
        else
        {
            textMeshProUGUI.gameObject.SetActive(false);
        }
    }
}
