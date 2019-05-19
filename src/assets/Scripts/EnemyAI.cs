using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public int health = 15;
	public GameObject crate;
	private int direction = 1;

	void Update(){
		if(transform.position.y < -5){
			if(direction > 0)
				transform.position = new Vector2(-2.33f, 4.74f);
			else if(direction < 0)
				transform.position = new Vector2(1.8f, 4.84f);
		}
		if(health <= 0){
			Instantiate(crate, transform.position, transform.rotation);
			Destroy(gameObject);
			
		}		
	}

	void OnCollisionEnter2D(Collision2D col){
		if("Wall".Equals(col.transform.tag) || "Enemy".Equals(col.transform.tag)){
			direction *= -1;
		}
		if("Grenade".Equals(col.transform.tag)){
			health = 0;
		}
		if("Rocket".Equals(col.transform.tag)){
			health = 0;
		}
	}

	void HitByGrenade(){
		health = 0;
	}

	void OnTriggerEnter2D(Collider2D col){
	
		if("Bullet".Equals(col.transform.tag)){
			health -= 5;
		}
		if("Rocket".Equals(col.transform.tag)){
			health = 0;
		}
	}
			
	void FixedUpdate () {
		transform.Translate(new Vector3 ( direction, 0, 0) * Time.deltaTime * 5f);
		transform.localScale = new Vector3(5 * direction, 5, 1);
	}
}
