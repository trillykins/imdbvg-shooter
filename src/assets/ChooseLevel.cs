using UnityEngine;
using System.Collections;

public class ChooseLevel : MonoBehaviour {

	public GameObject[] levels;

	void Awake () {
		Instantiate(levels[Random.Range(0, levels.Length)], transform.position, transform.rotation);	
	}
	
	public void getNewLevel(){
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Level");
		foreach (var gameObject in gameObjects) {
			Destroy(gameObject);
		}

		Instantiate(levels[Random.Range(0, levels.Length)], transform.position, transform.rotation);	
	}
}
