using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ChooseLevel : MonoBehaviour
{
    public GameObject[] levels;
    private readonly List<GameObject> levelPool = new List<GameObject>();

    void Awake()
    {
        foreach (var level in levels)
        {
            var l = Instantiate(level, transform.position, transform.rotation);
            l.SetActive(false);
            levelPool.Add(l);
        }
        levelPool[Random.Range(0, levels.Length)].SetActive(true);
    }

    public void GetNewLevel()
    {
        levelPool.ForEach(x => x.SetActive(false));
        var level = levelPool.ElementAt(Random.Range(0, levelPool.Count()));
        level.SetActive(true);
    }
}
