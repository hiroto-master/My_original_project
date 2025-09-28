using System;
using System.Linq;//id検索に使用
using UnityEngine;
using UnityEngine.UI;

public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] InventoryProd inventoryProd;
    [SerializeField] ItemDataProd itemDataProd;
    
    private bool isActive = false;
    private GameObject getItemObject;

    public Transform cameraTransform;
    float rayDistance = 5f;
    public Text interactionText;
    private void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, rayDistance);

        if (isHit && hit.collider.CompareTag("item"))
        {
            // アイテムに当たっているので、UIテキストを表示
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Eキーで入手";

            // さらにEキーが押されたら、アイテムを拾う
            if (Input.GetKeyDown(KeyCode.E))
            {
                getItemObject = hit.collider.gameObject;
                string getItemID = getItemObject.GetComponent<GetItem>().ReturnID();
                AddItem(getItemID);
                Destroy(getItemObject);
                isActive = false;
                getItemObject = null;
            }
        }
        else if (isHit && hit.collider.CompareTag("door"))
        {
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Eキーで開閉";
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<DoorController>().AnimateDoor();
            }
        }
        else
        {
            // UIテキストを非表示にする
            interactionText.gameObject.SetActive(false);
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
}
