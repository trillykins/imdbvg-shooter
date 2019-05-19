using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static bool isDead;
	public static string character;
	public static int score = 0;
	private AudioSource music;
	public AudioClip menuMusic;	
	public AudioClip gameMusic;	
	private bool hasChanged = false;
	
	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	void Start(){
		music = GetComponent<AudioSource>();
	}
	
	void Update(){
		if(Application.loadedLevel == 2 && !hasChanged){
			music.clip = gameMusic;
			music.Play();
			hasChanged = true;
		}
		if(Input.GetKeyDown(KeyCode.M)){
			music.mute = !music.mute;
		}
	}
	
	public void setName(string name){
		character = name;
	}
	
	public string getName(){
		return character;
	}
}
