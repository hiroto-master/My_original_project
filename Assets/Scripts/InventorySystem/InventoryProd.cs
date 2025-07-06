using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryProd : MonoBehaviour
{
    [SerializeField] ItemDataProd itemDataProd;//アイテムデータのすべての要素を持つ

    [SerializeField] Text itemNameText;
    [SerializeField] Text introduceText;

    public List<string> haveItemId = new List<string>();//取得したときにアイテムのIdを入れる

    private int showItemNum = 0;
    public FPMovement FPMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        showItemNum = 0;
        UpdateText(haveItemId[showItemNum]);//ゲットしたアイテムのすべての情報
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
        Item itemInfo = itemDataProd.ItemData.FirstOrDefault(a => a.ItemId == itemId);
        itemNameText.text = itemInfo.ItemName;
        introduceText.text = itemInfo.IntroduceText;
    }
}
