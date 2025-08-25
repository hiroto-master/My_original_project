using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField]string itemID;
    
    public string ReturnID()
    {
        return itemID;
    }
}
