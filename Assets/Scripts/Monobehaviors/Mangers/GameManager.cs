using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public GameObject[] SystemPrefabs;
    private SpawnPoint playerSpawnPoint;
    private Player player;

    void Start()
    {
        InstantiateSystemPrefabs();
        LevelManager.Instance.LoadLevel(0);
    }
    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
        }
    }

    public void SpawnPlayer()
    {
        playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpoiwnPoint").GetComponent<SpawnPoint>();
        if (player == null)
        {
            player = playerSpawnPoint.SpawnObject().GetComponent<Player>();
            if (!UIManager.Instance.IsUISpwaned())
                UIManager.Instance.SpawnUI();
            UIManager.Instance.healthBar.character = player;
        }
        else
        {
            player.transform.position = playerSpawnPoint.transform.position;
        }
        CameraManager.Instance.SetFollower(player.transform);
    }


    internal void OnPlyerKilled(Player p)
    {
        player = null;
        playerSpawnPoint.transform.position = p.transform.position;
        SpawnPlayer();
    }
}