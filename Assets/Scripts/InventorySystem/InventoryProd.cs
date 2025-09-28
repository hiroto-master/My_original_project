using System;
using System.Collections;
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
    public ParticleSystem particle;//除霊マスター

    public bool isGool = false;//ゴールできるかの判定

    [SerializeField] Image fadePanel;//フェードアウト用のパネル
    [SerializeField] Text fadeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && FPMovement.isOpenInventory)
        {
            if(haveItemId.Count == 0)return;
            OnClickBackButton();
        }
        if (Input.GetKeyDown(KeyCode.D) && FPMovement.isOpenInventory)
        {
            if (haveItemId.Count == 0)return;
            OnClickNextButton();
        }

        if (Input.GetKeyDown(KeyCode.E) && FPMovement.isOpenInventory)
        {
            if (haveItemId.Count == 0)return;
            //アイテムを装備する
            EquipmentItem();
        }

        if (Input.GetMouseButtonDown(0) && !FPMovement.isOpenInventory)
        {
            if (haveItemId.Count == 0)return;
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
            case "ohuda":
                equipmentItem = Instantiate(itemInfo.ItemImage,cameraroot);
                equipmentItem.transform.localPosition = new Vector3(0.82f, -0.2f, 1.046f);
                equipmentItem.transform.localRotation = Quaternion.Euler(-80.8f,90.6f,38.7f);
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
                Item zyoreimaster = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == "zyoreimaster");
                if (zyoreimaster.ItemCount >= 2)
                {
                    zyoreimaster.ItemCount -= 1;
                    
                    particle.Play();
                }
                else if (zyoreimaster.ItemCount == 1)
                {
                    zyoreimaster.ItemCount = 0;
                    haveItemId.Remove("zyoreimaster");
                    Destroy(equipmentItem);
                    equipmentItem = null;
                    
                    particle.Play();
                }
                break;
            case "energy":
                Item energy = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == "energy");
                if (energy.ItemCount >= 2)
                {
                    energy.ItemCount -= 1;
                }
                else if (energy.ItemCount == 1)
                {
                    energy.ItemCount = 0;
                    haveItemId.Remove("energy");
                    Destroy(equipmentItem);
                    equipmentItem = null;
                }
                FPMovement.currentState = FPMovement.PlayerState.UsedItem;
                break;
            case "ohuda":
                if (isGool)
                {
                    Debug.Log("Clear!!");
                    StartCoroutine(FadeOut());
                }
                break;
            default:
                break;
        }
    }

    public void Reset()
    {
        if (haveItemId.Count == 0)
        {
            if (previewObj != null)
            {
                Destroy(previewObj);
                previewObj = null;
            }
            itemNameText.text = "何も所持していない";
            introduceText.text = "";
            itemNumberText.text = "0/0";
            itemCountText.text = "";
        }
        else
        {
            showItemNum = Mathf.Clamp(showItemNum, 0, haveItemId.Count - 1);
            UpdateText(haveItemId[showItemNum]);
        }
    }

    IEnumerator FadeOut()
    {
        fadePanel.gameObject.SetActive(true);
        Color panelColor = fadePanel.color;
        // panelColor.a = 0;
        // fadePanel.color = panelColor;
        
        float elapsedTime = 0;
        while (elapsedTime < 2)//２はdurationTime
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / 2);
            panelColor.a = alpha;
            fadePanel.color = panelColor;
            yield return null;
        }
        yield return StartCoroutine(FadeInText());
    }
    IEnumerator FadeInText()
    {
        fadeText.gameObject.SetActive(true);
        Color textColor = fadeText.color;
        // textColor.a = 0;
        // fadeText.color = textColor;
        
        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime);
            textColor.a = alpha;
            fadeText.color = textColor;
            yield return null;
        }
    }
}
