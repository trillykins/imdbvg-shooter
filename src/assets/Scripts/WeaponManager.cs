using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

	public Sprite[] weapons;
	public GameObject[] projectiles;
	private Sprite currentWeapon;
	private string currentWeaponName;
	private AudioSource shoot;
	private float machinegunTimer = .1f;
	
			
	void Start () {
		currentWeapon = GetComponent<SpriteRenderer>().sprite;
		currentWeaponName = weapons[0].name;
		shoot = GetComponent<AudioSource>();	
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsDead){
			switch(currentWeaponName){
			case "Gun": 
				if(Input.GetButtonDown("Fire1")){
					
					GameObject bullet = Instantiate (projectiles[0], transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate (0, 0, UnityEngine.Random.Range (-3f, 4f));
					shoot.Play();
				}
				break;
			case "Machinegun": 
				machinegunTimer -= Time.deltaTime;
				if(Input.GetButton("Fire1") && machinegunTimer <= 0){
					GameObject bullet = Instantiate (projectiles[0], transform.position, transform.rotation) as GameObject;
					bullet.transform.Rotate (0, 0, UnityEngine.Random.Range (-3f, 4f));
					shoot.Play();
					machinegunTimer = .1f;
				}
				break;
			case "Grenadelauncher": 
				if(Input.GetButtonDown("Fire1")){
					Instantiate (projectiles[2], transform.position, transform.rotation); 
				}
				
				break;
			case "Rocketlauncher": 
				if(Input.GetButtonDown("Fire1")){
					Instantiate (projectiles[1], transform.position, transform.rotation); 
				}
				break;
			case "Shotgun":
				machinegunTimer -= Time.deltaTime;
				if (Input.GetButtonDown ("Fire1") && machinegunTimer <= 0) {
					for (int i = 0; i < 5; i++) {
						GameObject bullet = Instantiate (projectiles [0], transform.position, transform.rotation) as GameObject;
						bullet.GetComponent<DestroyOnCollision> ().speed = Random.Range (14f, 16f);
						bullet.transform.Rotate (0, 0, UnityEngine.Random.Range (-3f, 4f));
					}
					shoot.Play();
					machinegunTimer = .4f;
				}
				break;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if("Crate".Equals(col.transform.tag)){
			GameController.Score++;
			int bla = Random.Range(0, weapons.Length);
			GetComponent<SpriteRenderer>().sprite = weapons[bla];
			currentWeaponName = weapons[bla].name;	
			Destroy (col.gameObject);
			
		}
	}
	
	
	
}
