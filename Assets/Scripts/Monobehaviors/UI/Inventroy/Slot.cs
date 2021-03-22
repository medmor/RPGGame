using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    [SerializeField]
    private Item item = default;
    public Text qtyText;
    [HideInInspector]
    public Item slotItem;

    private void Start()
    {
        slotItem = ScriptableObject.CreateInstance<Item>();
        slotItem.itemType = item.itemType;
        slotItem.name = item.name;
        slotItem.sprite = item.sprite;
        slotItem.quantity = 0;
        slotItem.stackable = item.stackable;
    }
}