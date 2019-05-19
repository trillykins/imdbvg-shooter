using UnityEngine;
using System.Collections;

public class ChooseLevel : MonoBehaviour {

	public GameObject[] levels;

	void Awake () {
		Instantiate(levels[Random.Range(0, levels.Length)], transform.position, transform.rotation);	
	}
	
	public void getNewLevel(){
		GameObject[] pik = GameObject.FindGameObjectsWithTag("Level");
		for(int i = 0; i < pik.Length; i++)
			Destroy(pik[i]);
		
		Instantiate(levels[Random.Range(0, levels.Length)], transform.position, transform.rotation);	
	}
}
