using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryProd : MonoBehaviour
{
    [SerializeField] ItemDataProd itemDataProd;//アイテムデータ全ての要素が入っている

    [SerializeField] Text itemNameText;
    [SerializeField] Text introduceText;
    [SerializeField] private Text itemNumberText;
    [SerializeField] private Text itemCountText;
    public List<string> haveItemId = new List<string>();//取得した時にアイテムのIdを入れる

    private int showItemNum = 0;
    
    public FPMovement FPMovement;//プレイヤーの動作関連のスクリプト

    public Transform instansPos;

    private GameObject previewObj = null;
    
    private string currentEquipmentItem = null;

    public Transform cameraroot;//装備するときの親のオブジェクト
    private GameObject equipmentItem = null;//手に持っているアイテム
    public ParticleSystem particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showItemNum = 0;
        UpdateText(haveItemId[showItemNum]);//取得したアイテムID
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UpdateText(haveItemId[showItemNum]);
        }
        if (Input.GetKeyDown(KeyCode.A) && FPMovement.isOpenInventory)
        {
            OnClickBackButton();
        }
        if (Input.GetKeyDown(KeyCode.D) && FPMovement.isOpenInventory)
        {
            OnClickNextButton();
        }

        if (Input.GetKeyDown(KeyCode.E) && FPMovement.isOpenInventory)
        {
            //アイテムを装備する
            EquipmentItem();
        }

        if (Input.GetMouseButton(0) && !FPMovement.isOpenInventory)
        {
            UseItem();
        }
    }
    public void OnClickNextButton()
    {
        showItemNum++;
        if (showItemNum > haveItemId.Count - 1)
        {
            showItemNum = 0;
        }
        UpdateText(haveItemId[showItemNum]);//アイテムの名前を入れる
    }
    public void OnClickBackButton()
    {
        showItemNum--;
        if (showItemNum < 0)
        {
            showItemNum = haveItemId.Count - 1;
        }
        UpdateText(haveItemId[showItemNum]);
    }
    private void UpdateText(string itemId)
    {
        Item itemInfo = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == itemId);//idからアイテムを探す
        itemNameText.text = itemInfo.ItemName;
        introduceText.text = itemInfo.IntroduceText;
        itemNumberText.text = showItemNum+1 +  "/" + haveItemId.Count;
        itemCountText.text = "所持数：" + itemInfo.ItemCount;
        if (previewObj != null)
        {
            Destroy(previewObj);
        }
        previewObj = Instantiate(itemInfo.ItemImage,instansPos.position,itemInfo.ItemImage.transform.rotation);
    }
    public void EquipmentItem()//アイテムを装備するスクリプト
    {
        currentEquipmentItem = haveItemId[showItemNum];//appleなどが入る
        if (equipmentItem !=null)Destroy(equipmentItem);//表示しているものを削除
        Item itemInfo = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == currentEquipmentItem);//idからアイテムを探す
        switch (currentEquipmentItem)
        {
            case "zyoreimaster":
            {
                equipmentItem = Instantiate(itemInfo.ItemImage,cameraroot);
                equipmentItem.transform.localPosition = new Vector3(0.55f, -0.5f, 0.795f);
                equipmentItem.transform.localRotation = Quaternion.Euler(0,82,0);
                break;
            }
            case "energy":
                equipmentItem = Instantiate(itemInfo.ItemImage,cameraroot);
                equipmentItem.transform.localPosition = new Vector3(0.8f, -0.4f, 1.04f);
                equipmentItem.transform.localRotation = Quaternion.Euler(-16.48f,224,0);
                break;
            default:
                break;
        }
        FPMovement.CloseInventory();
    }
    private void UseItem()
    {
        switch (currentEquipmentItem)
        {
            case "zyoreimaster":
                particle.Play();
                break;
            case "melon":
                Debug.Log("melon");
                FPMovement.isUseEnergy = true;
                FPMovement.onece = true;
                break;
            default:
                break;
        }
    }
}
