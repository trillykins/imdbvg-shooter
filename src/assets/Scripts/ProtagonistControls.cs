using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProtagonistControls : MonoBehaviour {

	public GameObject explosion;
	private AudioSource boom;
	public float movementSpeed = 1f;
	public Canvas deathScreen;
	public Button retry;
	private string character;
	private bool grounded;
	private int direction;
	public bool isDead = false;
	private int ZukoVariable = 1;
	
	void Awake(){
		character = GameController.character;
		boom = GetComponent<AudioSource>();	
		
	}
	
	void Start(){
		deathScreen = deathScreen.GetComponent<Canvas>();
		retry = retry.GetComponent<Button>();
		deathScreen.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!isDead){
			switch(character){
			case "HeisenbergLives" : 
				if(GameObject.FindGameObjectsWithTag("Enemy").Length > 10)
					Death();
				break;
			case "nana_strikes" :
				if(Random.Range(1, 1000) > 995){
					Explode();
				}
				break;
			case "Firelord_Zuko" :
				if(Random.Range(1, 1000) > 995){
					ZukoVariable *= -1;
				}
				break;
			}
		
			if(Input.GetAxisRaw("Horizontal") != 0){
				if(Input.GetAxisRaw("Horizontal") > 0)
					direction = 1;
				else
					direction = -1;
				transform.Translate(new Vector3 ( direction * ZukoVariable, 0, 0) * Time.deltaTime * movementSpeed);
				transform.localScale = new Vector3(-5 * direction * ZukoVariable, 5, 1);
				
			}
		}
	}

	void Update(){
		if(!isDead){
			if(Input.GetButtonDown("Jump") && grounded){
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				GetComponent<Rigidbody2D>().angularVelocity = 0;
				GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800f);
				grounded = false;
			}
			if("Solaris666".Equals(GameController.character) && Input.GetButtonDown("Fire1")){
				if(Random.Range(1, 1000) > 995){
					Death();
				}
			}
			if(transform.position.y < -5 || transform.position.y > 5){
				Death();
			}
		}
		else{
			if(transform.position.y < -6){
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			}
		}
	}	

	void OnCollisionEnter2D (Collision2D col){
		if("Floor".Equals(col.transform.tag))
			grounded = true;
		if("Enemy".Equals(col.transform.tag)){
			Destroy (col.gameObject);
			Death();
		}
	}
	
	bool IsGrounded() {
		Debug.DrawRay(transform.position, -Vector2.up, Color.green);
		return Physics2D.Raycast(transform.position, -Vector2.up, 1f);
	}
	
	void Explode(){
		Instantiate(explosion, transform.position, transform.rotation);
		boom.Play();
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
		if(colliders.Length > 0){
			for(int i = 0; i < colliders.Length; i++){
				if("Enemy".Equals(colliders[i].tag) || "Crate".Equals(colliders[i].tag))
					Destroy(colliders[i].gameObject);
			}
		}
	}

	void Death(){
		GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800f);
		GetComponent<Rigidbody2D>().AddForce(Vector2.right * 120f);
		GetComponent<Collider2D>().isTrigger = true;
		GameController.isDead = true;
		isDead = true;
		deathScreen.enabled = true;
		retry.enabled = true;
	}
	
	public void Reset(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
		for(int i = 0; i < enemies.Length; i++)
			Destroy(enemies[i]);
		for(int i = 0; i < crates.Length; i++)
			Destroy(crates[i]);
		GetComponent<Collider2D>().isTrigger = false;
		GameController.score = 0;
		transform.position = GameObject.FindWithTag("Respawn").transform.position;
		deathScreen.enabled = false;
		retry.enabled = false;
		ZukoVariable = 1;
		GetComponent<Rigidbody2D>().isKinematic = false;
		GameController.isDead = false;
		GameObject.FindWithTag("LevelChooser").SendMessage("getNewLevel");
		isDead = false;
	}
	
	public void ExitGame(){
		GameController.isDead = false;
		GameController.score = 0;
		Destroy(GameObject.FindWithTag("GameController"));
		Application.LoadLevel(0);
	}
}
