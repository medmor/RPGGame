using UnityEngine;
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string objectName;
    public Sprite sprite;
    public int quantity;
    public bool stackable;
    public enum ItemType
    {
        COIN,
        HEALTH,
        MAP,
        SLINGSHOT,
        AXE,
        AMMO
    }
    public ItemType itemType;
}