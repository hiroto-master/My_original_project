using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    public int itemId;
    public GameObject itemPrefab;
    public string itemName;
    [TextArea]
    public string itemIntroduce;
}
