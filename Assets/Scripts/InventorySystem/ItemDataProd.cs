using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataProd", menuName = "ScriptableObjects/CreateItemdata")]
public class ItemDataProd : ScriptableObject
{
    public List<Item> ItemData = new List<Item>();
}

[System.Serializable]
public class Item //�V�����^�����@�S�Ă̏��̓������^
{
    [SerializeField]private string _itemName;
    public string ItemName => _itemName;

    [SerializeField]private string _introduceText;
    public string IntroduceText => _introduceText;

    [SerializeField]private string _itemId;
    public string ItemId => _itemId;

    [SerializeField]private GameObject _itemImage;
    public GameObject ItemImage => _itemImage;

    [SerializeField]private int _itemcount;
    public int ItemCount
    {
        get => _itemcount;
        set => _itemcount = value;
    }
}
