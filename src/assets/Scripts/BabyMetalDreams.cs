using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BabyMetalDreams : MonoBehaviour {

	public Image baby;

	void Start(){
		baby = baby.GetComponent<Image> ();
	}

	void Update () {
		if("Space_Wolf27".Equals(GameController.character)){
			Color splat = baby.color;
			splat.a = Mathf.PingPong(Time.time * 0.05f, .2f);
			baby.color = splat;
		}
	}
}
