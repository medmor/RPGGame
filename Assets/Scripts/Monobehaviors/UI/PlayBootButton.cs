using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBootButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        LevelManager.Instance.LoadLevel(1);
    }
}
