using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour {

	public float speed = 15f;
	private float direction;

	void Awake() {
		GameObject hat = new GameObject();
		foreach(GameObject go in GameObject.FindGameObjectsWithTag ("Player")){
			if(go.name.Equals("Protagonist")){
				hat = go;
			}
		}
		if(hat.transform.localScale.x < 0){
			direction = -1f;
		} else {
			direction = 1f;
		}
	}

	void Update(){
		transform.Translate(new Vector3(direction, 0, 0) * Time.deltaTime * speed);
		transform.localScale = new Vector3(-7 * direction, 7, 1);
//		Vector3 pos = transform.position;
//		pos.z = 0;
//		transform.position = pos;
	}
/*
	void OnTriggerEnter2D (Collider2D col){
		if(!"Player".Equals(col.tag))
			Destroy (gameObject);
	}
*/
}
