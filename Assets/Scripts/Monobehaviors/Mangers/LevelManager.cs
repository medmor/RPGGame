using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    private int LevelIndex = -1;
    private const string baseLevelUrl = "Levels/";
    internal readonly List<string> Levels = new List<string> { "Boot", "level1", "Level2" };
    private List<GameObject> LevelsObjects = new List<GameObject>();


    internal void LoadLevel(int index)
    {
        if (index == LevelIndex) return;

        GameObject level = Resources.Load<GameObject>(baseLevelUrl + Levels[index]);

        if (!LevelsObjects.Find(l => l.name == level.name + "(Clone)"))
            LevelsObjects.Add(Instantiate(level));

        level = LevelsObjects[index];
        level.SetActive(true);

        CameraManager.Instance.SetConfiner(level.GetComponent<Collider2D>());

        if (LevelIndex != -1)
            LevelsObjects[LevelIndex].SetActive(false);

        LevelIndex = index;

        if (LevelIndex > 0)
            GameManager.Instance.SpawnPlayer();


    }


}
