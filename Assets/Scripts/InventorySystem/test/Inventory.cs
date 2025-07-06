using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]ItemDataBase itemDataBase;

    [SerializeField] Text itemNameText;
    [SerializeField] Text introduceText;

    public List<int>itemID = new List<int>();

    private int showItemNum = 0;
    public FPMovement FPMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("currentID:"+ itemID[showItemNum]);
        UpdateText(itemID[showItemNum]);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && FPMovement.isOpenInventory)
        {
            OnClickBackButton();
        }
        if(Input.GetKeyDown(KeyCode.D) && FPMovement.isOpenInventory)
        {
            OnClickNextButton();
        }
    }
    public void OnClickNextButton()
    {   
        showItemNum++;
        if (showItemNum > itemID.Count-1)
        {
            showItemNum = 0;
        }
        Debug.Log("currentID:" + itemID[showItemNum]);
        UpdateText(itemID[showItemNum]);
    }
    public void OnClickBackButton()
    {
        showItemNum--;
        if (showItemNum < 0)
        {
            showItemNum = itemID.Count-1;
        }
        Debug.Log("currentID:" + itemID[showItemNum]);
        UpdateText(itemID[showItemNum]);
    }
    private void UpdateText(int itemNum)
    {
        itemNameText.text = itemDataBase.itemData[itemNum].itemName;
        introduceText.text = itemDataBase.itemData[itemNum].itemIntroduce;
    }
}
