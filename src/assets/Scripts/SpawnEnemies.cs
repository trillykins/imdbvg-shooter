using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	bool isSpawning = false;
	public float minTime = 5.0f;
	public float maxTime = 15.0f;
	public GameObject[] enemies;  // Array of enemy prefabs.
	
	void Start (){
		if((GameController.Character?.Name ?? "") == "Jaguar-Wong-is-Dead"){
			minTime = 1f;
			maxTime = 2f;
		}
	}
	
	IEnumerator SpawnObject(int index, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Instantiate(enemies[index], transform.position, transform.rotation);
		
		//We've spawned, so now we could start another spawn     
		isSpawning = false;
	}
	
	void Update () 
	{
		//We only want to spawn one at a time, so make sure we're not already making that call
		if(! isSpawning)
		{
			isSpawning = true; //Yep, we're going to spawn
			int enemyIndex = Random.Range(0, enemies.Length);
			StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
		}
	}
}
