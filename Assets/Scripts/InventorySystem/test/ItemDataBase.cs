using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> itemData = new List<ItemData>();
}
