using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SpawnEnemies : MonoBehaviour
{
    public float minTime = 5.0f;
    public float maxTime = 10.0f;
    public GameObject[] enemies;  // Array of enemy prefabs.
    public GameObject Crate;

    private static readonly List<GameObject> _enemyPool = new List<GameObject>();
    private static readonly List<GameObject> _cratePool = new List<GameObject>();
    private static int _crateIndex = 0;
    private static int _enemyIndex = 0;

    private DateTime timeOut;

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            var crate = Instantiate(Crate, transform.position, transform.rotation);
            var enemy = Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], transform.position, transform.rotation);
            enemy.SetActive(false);
            crate.SetActive(false);
            _enemyPool.Add(enemy);
            _cratePool.Add(crate);
        }
        if ((GameController.Character?.Name ?? "") == "Jaguar-Wong-is-Dead")
        {
            minTime = 1f;
            maxTime = 2f;
        }
    }

    IEnumerator SpawnObject(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SpawnObject(_enemyPool, ref _enemyIndex, transform);
    }

    void Update()
    {
        //We only want to spawn one at a time, so make sure we're not already making that call
        if (timeOut < DateTime.UtcNow)
        {
            var cooldown = UnityEngine.Random.Range(minTime, maxTime);
            timeOut = DateTime.UtcNow.AddSeconds(cooldown);
            StartCoroutine(SpawnObject(cooldown));
        }
    }

    private static void SpawnObject(List<GameObject> gameObjects, ref int index, Transform transform)
    {
        var o = gameObjects[index++];
        o.transform.SetPositionAndRotation(transform.position, transform.rotation);
        o.SetActive(true);
        if (index >= gameObjects.Count()) index = 0;
    }

    public static void SpawnCrate(Transform transform)
    {
        Debug.Log("Spawning crate!");
        SpawnObject(_cratePool, ref _crateIndex, transform);
    }
}
