using UnityEngine;
using System.Collections;

public class InfernoBucket : MonoBehaviour {

	private SpriteRenderer hat;
	
	void Start () {
		hat = GetComponent<SpriteRenderer>();
		if("theinfernobucket".Equals(GameController.Character))
			hat.enabled = true;
		else
			hat.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
