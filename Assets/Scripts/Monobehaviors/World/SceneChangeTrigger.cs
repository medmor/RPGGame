using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public int SceneToLoad;
    public Transform newSpoinPoint;
    public Transform previousSpoinPoint;

    void ChangeScene()
    {
        LevelManager.Instance.LoadLevel(SceneToLoad);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (newSpoinPoint) 
            {
                previousSpoinPoint.position = newSpoinPoint.position;
            }
            ChangeScene();
        }
    }
}
