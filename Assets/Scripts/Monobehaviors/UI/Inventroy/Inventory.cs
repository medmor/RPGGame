using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Inventory : MonoBehaviour
{
    PlayerAttack playerAttack;
    public GameObject slotPrefab;
    public List<Slot> slots = new List<Slot>();
    List<Item.ItemType> weapons = new List<Item.ItemType>();
    public void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    public bool AddItem(Item itemToAdd)
    {
        switch (itemToAdd.itemType)
        {
            case Item.ItemType.COIN:
                if (itemToAdd.quantity > 99) return false;
                return AddCoinAmmo(itemToAdd);
            case Item.ItemType.MAP:
                return AddMap(itemToAdd);
            case Item.ItemType.SLINGSHOT:
                playerAttack.AddWeapon(new SlingshotWeapon(playerAttack));
                return AddWeapon(itemToAdd);
            case Item.ItemType.AXE:
                playerAttack.AddWeapon(new AxeWeapon(playerAttack));
                return AddWeapon(itemToAdd);
            case Item.ItemType.AMMO:
                return AddCoinAmmo(itemToAdd);
            default:
                break;
        }
        return false;
    }
    private bool AddCoinAmmo(Item itemToAdd)
    {
        Slot coinAmmoSlot = FindSlot(itemToAdd.itemType);
        coinAmmoSlot.slotItem.quantity += itemToAdd.quantity;
        coinAmmoSlot.qtyText.text = coinAmmoSlot.slotItem.quantity.ToString();
        return true;
    }
    private bool AddMap(Item itemToAdd)
    {
        var map = GameObject.FindGameObjectWithTag("Map");
        var image = map.GetComponent<Image>();
        image.sprite = itemToAdd.sprite;
        Slot buttonSlot = FindSlot(Item.ItemType.MAP);
        Image buttonImage = buttonSlot.transform.GetChild(1).GetComponent<Image>();
        buttonImage.color = new Color(1, 1, 1, 1);
        return true;
    }
    private bool AddWeapon(Item itemToAdd)
    {
        foreach(Item.ItemType t in weapons)
        {
            changeButtonSlotImageAlpha(t, .4f);
        }
        changeButtonSlotImageAlpha(itemToAdd.itemType, 1f);
        weapons.Add(itemToAdd.itemType);
        return true;
    }
    private Slot FindSlot(Item.ItemType type)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].slotItem.itemType == type)
            {
                return slots[i];
            }
        }
        return null;
    }
    private Image GetSlotImageComponent(Transform slotTransform)
    {
        return slotTransform.GetChild(1).GetComponent<Image>();
    }
    public bool hasWeapon(Item.ItemType w)
    {
        return weapons.Contains(w);
    }
    void changeButtonSlotImageAlpha(Item.ItemType weaponType, float alpha)
    {
        Slot buttonSlot = FindSlot(weaponType);
        Image buttonImage = buttonSlot.transform.GetChild(1).GetComponent<Image>();
        buttonImage.color = new Color(1, 1, 1, alpha);
    }
    public int GetNumberOfAmmo()
    {
        var ammoSlot = FindSlot(Item.ItemType.AMMO);
        return ammoSlot.slotItem.quantity;
    }
    public void SubstractCoinAmmo(Item.ItemType type, int count)
    {
        Slot coinAmmoSlot = FindSlot(type);
        coinAmmoSlot.slotItem.quantity -= count;
        coinAmmoSlot.qtyText.text = coinAmmoSlot.slotItem.quantity.ToString();
    }

}
