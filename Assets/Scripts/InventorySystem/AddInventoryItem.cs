using System;
using System.Linq;//id検索に使用
using UnityEngine;

public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] InventoryProd inventoryProd;
    [SerializeField] ItemDataProd itemDataProd;
    
    private bool isActive = false;
    private GameObject getItemObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
        {
            string getItemID = getItemObject.GetComponent<GetItem>().ReturnID();
            AddItem(getItemID);
            Destroy(getItemObject);
            isActive = false;
            getItemObject = null;
        }
    }

    void AddItem(string sendID)//アイテムIDからアイテムを追加する
    {
        if (inventoryProd.haveItemId.Contains(sendID))
        {
            Item itemInfo = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == sendID);//idからアイテムを探す
            itemInfo.ItemCount = itemInfo.ItemCount + 1;
        }
        else
        {
            Item itemInfo = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == sendID);//idからアイテムを探す
            itemInfo.ItemCount = 1;
            inventoryProd.haveItemId.Insert(0, sendID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            isActive = true;
            getItemObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            isActive = false;
            getItemObject = null;
        }
    }
}
