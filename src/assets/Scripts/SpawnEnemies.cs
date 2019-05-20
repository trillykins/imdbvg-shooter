using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnEnemies : MonoBehaviour
{
    bool isSpawning = false;
    public float minTime = 5.0f;
    public float maxTime = 15.0f;
    public GameObject[] enemies;  // Array of enemy prefabs.
    public GameObject Crate;

    private readonly List<GameObject> _enemyPool = new List<GameObject>();
    private readonly List<GameObject> _cratePool = new List<GameObject>();
    private int _enemyIndex = 0;

    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            var crate = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, transform.rotation);
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, transform.rotation);
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
        //Instantiate(enemies[index], transform.position, transform.rotation);
        var enemy = _enemyPool[_enemyIndex++];
        enemy.transform.SetPositionAndRotation(transform.position, transform.rotation);
        enemy.SetActive(true);
        if (_enemyIndex >= _enemyPool.Count()) _enemyIndex = 0;

        //We've spawned, so now we could start another spawn     
        isSpawning = false;
    }

    void Update()
    {
        //We only want to spawn one at a time, so make sure we're not already making that call
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnObject(Random.Range(minTime, maxTime)));
        }
    }
}
