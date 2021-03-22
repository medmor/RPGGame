using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    [SerializeField]
    GameObject inventoryPrefab = default;
    [SerializeField]
    GameObject healthBarPrefab = default;
    [SerializeField]
    GameObject joystickPrefab = default;

    [HideInInspector]
    public Inventory inventory;
    [HideInInspector]
    public HealthBar healthBar;
    [HideInInspector]
    public Joystick joystick;

    internal void SpawnUI()
    {
        SpawnInventory();
        SpawnHealthBar();
        SpawnJoystick();
    }

    internal void DestroyUI()
    {
        Destroy(inventory);
        Destroy(healthBar);
        Destroy(joystick);
    }

    internal bool IsUISpwaned()
    {
        return inventory != null;
    }

    void SpawnInventory()
    {
        inventory = Instantiate(inventoryPrefab).GetComponent<Inventory>();
        inventory.transform.SetParent(transform);
    }


    void SpawnHealthBar()
    {
        healthBar = Instantiate(healthBarPrefab).GetComponent<HealthBar>();
        healthBar.transform.SetParent(transform);
    }

    void SpawnJoystick()
    {
        joystick = Instantiate(joystickPrefab).GetComponentInChildren<Joystick>();
        joystick.transform.parent.SetParent(transform);
    }

    internal void HideUI()
    {
        gameObject.SetActive(false);
    }

    internal void ShowUI()
    {
        gameObject.SetActive(true);
    }
}
