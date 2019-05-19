using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class nana_strikes : MonoBehaviour {

	public Image truman;

	void Start () {
		truman = truman.GetComponent<Image>();
		if("nana_strikes".Equals(GameController.Character)){
			truman.enabled = true;
		}
		else
			truman.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
