using System.Linq;//id検索に使用
using UnityEngine;

public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] InventoryProd inventoryProd;
    [SerializeField] ItemDataProd itemDataProd;
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddItem("apple");
        }
    }

    void AddItem(string sendID)
    {
        if (inventoryProd.haveItemId.Contains(sendID))
        {
            Item itemInfo = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == sendID);//idからアイテムを探す
            itemInfo.ItemCount = itemInfo.ItemCount + 1;
        }
        else
        {
            inventoryProd.haveItemId.Insert(0, sendID);
        }
    }
}
