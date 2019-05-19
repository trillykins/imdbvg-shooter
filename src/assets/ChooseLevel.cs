using UnityEngine;
using System.Linq;
using System.Collections;

public class ChooseLevel : MonoBehaviour
{
    public GameObject[] levels;

    void Awake()
    {
        Instantiate(levels[Random.Range(0, levels.Length)], transform.position, transform.rotation);
    }

    public void GetNewLevel()
    {
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("Level"))
        {
            Destroy(gameObject);
        }

        Instantiate(levels[Random.Range(0, levels.Length)], transform.position, transform.rotation);
    }
}
