using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	private int selectionIndex;
	private GameObject gameController;

	public Sprite[] avatars;
	public string[] charactersDescription;
	public string[] charactersName;
	
	public Canvas selectionMenu;
	public Image avatar;
	public Text characterName;
	public Text characterDescription;
	public Button startButton;
	
	void Start ()
		
	{
		gameController = GameObject.FindWithTag("GameController");
		avatar.sprite = avatars[selectionIndex];
		characterDescription.text = charactersDescription[selectionIndex];
		characterName.text = charactersName[selectionIndex];

		selectionMenu = selectionMenu.GetComponent<Canvas>();
		
	}
	
	public void LeftArrowPress() //this function will be used on our Exit button
		
	{
		if(selectionIndex < avatars.Length)
			selectionIndex++;
		if(selectionIndex == avatars.Length)
			selectionIndex = 0;
		avatar.sprite = avatars[selectionIndex];
		characterDescription.text = charactersDescription[selectionIndex];
		characterName.text = charactersName[selectionIndex];
		if("Knight_Artorias".Equals(characterName.text)){
			startButton.enabled = false;
		}
		else
			startButton.enabled = true;
	}
	
	public void RightArrowPress() //this function will be used for our "NO" button in our Quit Menu
		
	{
		if(selectionIndex < avatars.Length)
			selectionIndex--;
		if(selectionIndex < 0)
			selectionIndex = avatars.Length-1;
		avatar.sprite = avatars[selectionIndex];
		characterDescription.text = charactersDescription[selectionIndex];
		characterName.text = charactersName[selectionIndex];
		if("Knight_Artorias".Equals(characterName.text)){
			startButton.enabled = false;
		}
		else
			startButton.enabled = true;
		
	}
	
	public void StartLevel () //this function will be used on our Play button
		
	{
		gameController.SendMessage("setName", characterName.text);
		Application.LoadLevel (2); //this will load our first level from our build settings. "1" is the second scene in our game
		
	}	
}
