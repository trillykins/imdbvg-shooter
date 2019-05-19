using UnityEngine;
using System.Collections;

public class Grenadelauncher : MonoBehaviour {
	
	private Rigidbody2D rigid;
	private float direction;
	public GameObject explosion;
	private AudioSource boom;
	public AudioClip pik;
	private float timer = 2f;
	
	void Awake(){
		boom = GetComponent<AudioSource>();			
		if(GameObject.FindWithTag("Player").transform.localScale.x < 0)
			direction = -1f;
		else
			direction = 1f;
	}
	
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		rigid.AddForce(Vector2.up * 450f);
		if(direction < 0)
			rigid.AddForce(Vector2.right * 500f);
		else if(direction > 0)
			rigid.AddForce(Vector2.right * -500f);
	}
	
	void Update(){
		timer -= Time.deltaTime;
		if (timer <= 0f && GetComponent<Collider2D>().enabled == true){
			Instantiate(explosion, transform.position, transform.rotation);
			boom.PlayOneShot(pik);
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
			if(colliders.Length > 0){
				for(int i = 0; i < colliders.Length; i++){
					if("Enemy".Equals(colliders[i].tag) || "Crate".Equals(colliders[i].tag))
						Destroy(colliders[i].gameObject);
					if("Player".Equals(colliders[i].tag)){
						GameObject.FindWithTag("Player").SendMessage("Death");
					}
				}
			}
			gameObject.GetComponent<Renderer>().enabled = false;	
			GetComponent<Collider2D>().enabled = false;
			Destroy(gameObject, 1f);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if("Enemy".Equals(col.transform.tag) || "Crate".Equals(col.transform.tag)){
			Instantiate(explosion, transform.position, transform.rotation);
			boom.PlayOneShot(pik);
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
			if(colliders.Length > 0){
				for(int i = 0; i < colliders.Length; i++){
					if("Enemy".Equals(colliders[i].tag))
						colliders[i].gameObject.SendMessage("HitByGrenade");
					if("Crate".Equals(colliders[i].tag))
						Destroy (colliders[i].gameObject);	
					if("Player".Equals(colliders[i].tag)){
						GameObject.FindWithTag("Player").SendMessage("Death");
					}
				}
			}
			gameObject.GetComponent<Renderer>().enabled = false;	
			GetComponent<Collider2D>().enabled = false;
			Camera.main.GetComponent<CameraShake> ().ShakeCamera (0.1f, 1f);
			Destroy(gameObject, 1f);
		}
	}
}
