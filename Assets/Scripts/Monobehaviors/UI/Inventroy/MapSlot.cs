using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSlot : MonoBehaviour
{
    public GameObject map;
    public void ToggelMap()
    {
        //var map = GameObject.FindGameObjectWithTag("Map");
        map.SetActive(!map.activeSelf);
    }
}
