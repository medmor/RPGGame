using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    private Item.ItemType type;
    private BaseAttack playerAttack;
    GameObject playerObject;
    private Player player;
    public List<Slot> otherSlots;

    public void Start()
    {
        type = GetComponent<Slot>().slotItem.itemType;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        playerAttack = playerObject.GetComponent<BaseAttack>();
    }
    public void ChangeWeapon()
    {
        if (type == Item.ItemType.AXE && UIManager.Instance.inventory.hasWeapon(Item.ItemType.AXE))
        {
            playerAttack.ChangeWeapon(typeof(AxeWeapon));
            UpdateWeaponsSlotsImages();
        }
        if (type == Item.ItemType.SLINGSHOT && UIManager.Instance.inventory.hasWeapon(Item.ItemType.SLINGSHOT))
        {
            playerAttack.ChangeWeapon(typeof(SlingshotWeapon));
            UpdateWeaponsSlotsImages();
        }
    }

    private void UpdateWeaponsSlotsImages()
    {
        var image = transform.GetChild(1).GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 255);
        foreach (Slot s in otherSlots)
        {
            var i = s.transform.GetChild(1).GetComponent<Image>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, .4f);
        }
    }
}
