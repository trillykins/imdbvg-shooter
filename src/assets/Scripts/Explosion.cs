using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public GameObject explosion;
	public AudioClip pik;

	private AudioSource _boom;
    private ProtagonistControls _player;

    void Start () {
        _player = FindObjectOfType<ProtagonistControls>();
        _boom = GetComponent<AudioSource>();	
	}	
		
	void OnTriggerEnter2D (Collider2D col){
		if(!"Player".Equals(col.tag)){
			Instantiate(explosion, transform.position, transform.rotation);
			_boom.PlayOneShot(pik);
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
			if(colliders.Length > 0){
				for(int i = 0; i < colliders.Length; i++){
					if("Enemy".Equals(colliders[i].tag))
						colliders[i].gameObject.SendMessage("HitByGrenade");
					if("Crate".Equals(colliders[i].tag))
						colliders[i].gameObject.SetActive(false);	
						//Destroy (colliders[i].gameObject);	
					if("Player".Equals(colliders[i].tag)){
                        _player.Death();
						//GameObject.FindWithTag("Player").SendMessage("Death");
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
