using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotWeapon : BaseWeapon
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize = 5;
    public float weaponVelocity = 5;


    public SlingshotWeapon(BaseAttack p)
    {
        AnimationBoolName = "isFiring";
        Parent = p;
        //inventory = p.gameObject.GetComponent<Player>().inventory;
        ammoPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = MonoBehaviour.Instantiate(Resources.Load("AmmoObject")) as GameObject;
            ammoObject.transform.parent = Parent.transform;
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
        Item ammos = ScriptableObject.CreateInstance<Item>();
        ammos.quantity = 10;
        ammos.itemType = Item.ItemType.AMMO;
        UIManager.Instance.inventory.AddItem(ammos);
    }
    internal override bool IsReady()
    {

        return UIManager.Instance.inventory.GetNumberOfAmmo() > 0;
    }
    internal override void UseWeapon(Vector3 pos)
    {
        FireAmmo(pos);
    }
    public GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }
    void FireAmmo(Vector3 mousePosition)
    {
        GameObject ammo = SpawnAmmo(Parent.transform.position);

        Arc arcScript = ammo.GetComponent<Arc>();
        float travelDuration = 1.0f / weaponVelocity;
        Parent.StartCoroutine(arcScript.TravelArc(mousePosition, travelDuration));
        UIManager.Instance.inventory.SubstractCoinAmmo(Item.ItemType.AMMO, 1);
    }
    void OnDestroy()
    {
        ammoPool = null;
    }


}
