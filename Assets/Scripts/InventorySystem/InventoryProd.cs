using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryProd : MonoBehaviour
{
    [SerializeField] ItemDataProd itemDataProd;

    [SerializeField] Text itemNameText;
    [SerializeField] Text introduceText;

    public List<int> inventoryItems = new List<int>();

    private int showItemNum = 0;
    public FPMovement FPMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("currentID:" + inventoryItems[showItemNum]);
        UpdateText(inventoryItems[showItemNum]);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && FPMovement.isOpenInventory)
        {
            OnClickBackButton();
        }
        if (Input.GetKeyDown(KeyCode.D) && FPMovement.isOpenInventory)
        {
            OnClickNextButton();
        }
    }
    public void OnClickNextButton()
    {
        showItemNum++;
        if (showItemNum > inventoryItems.Count - 1)
        {
            showItemNum = 0;
        }
        Debug.Log("currentID:" + inventoryItems[showItemNum]);
        UpdateText(inventoryItems[showItemNum]);
    }
    public void OnClickBackButton()
    {
        showItemNum--;
        if (showItemNum < 0)
        {
            showItemNum = inventoryItems.Count - 1;
        }
        Debug.Log("currentID:" + inventoryItems[showItemNum]);
        UpdateText(inventoryItems[showItemNum]);
    }
    private void UpdateText(int itemNum)
    {
        itemNameText.text = itemDataProd.ItemData[itemNum].ItemName;
        introduceText.text = itemDataProd.ItemData[itemNum].IntroduceText;
    }
}
