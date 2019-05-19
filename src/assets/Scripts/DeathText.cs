using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathText : MonoBehaviour {

	public Text text;

	void Start (){
		text = text.GetComponent<Text>();
		switch(GameController.character){
		case "HeisenbergLives" : 
			text.text = "The pressure got too much for poor, Heisenberg. With his last breath, he proclaimed the game to be terrible and that he'd return to make a thread about it.";
			break;
		case "nana_strikes" :
			text.text = "";
			break;
		case "Firelord_Zuko" :
			text.text = "Huh? What's going on? The Last of Us is the greatest and innovatest game of all time. I'm serious! Stop laughing at me!";
			break;
		case "Solaris666" :
			text.text = "FUCKING MOOSLIM TERRORISTS!!! I AIN'T NO RACIST BUT ALL 'DEM DARKIES NEED A SOLID BUMMIN'!!!";
			break;
		case "Jaguar-Wong-is-Dead" :
			text.text = "Pff, too easy. Couldn't be bothered";
			break;
		case "Space_Wolf27" :
			text.text = "Fuck it, man, I'm too alpha for this poorly made shit. I'm moving to Japan anyway to creep on Japanese ladies";
			break;
		case "OurGloriousLeader" :
			text.text = "Concession noted";
			break;
		case "theinfernobucket" :
			text.text = "Lost like the Swedish football team does once in a while to Denmark. It is said that his ugly pirate hat still roams the internet in search of a replacement Swede";
			break;
			
		}
	}

}
