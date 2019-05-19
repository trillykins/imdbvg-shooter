using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class CharacterSelection : MonoBehaviour
{
    private int characterIndex;
    private GameObject gameController;

    public List<Character> Characters;

    //public Sprite[] avatars;
    //public string[] charactersDescription;
    //public string[] charactersName;

    public Canvas SelectionMenu;
    public Image Avatar;
    public Text CharacterName;
    public Text CharacterDescription;
    public Button StartButton;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        SelectionMenu = SelectionMenu.GetComponent<Canvas>();

        characterIndex = UnityEngine.Random.Range(0, Characters.Count());
        SetCurrentAvatar();
    }

    private void SetCurrentAvatar()
    {
        var index = Mathf.Abs(characterIndex);
        Avatar.sprite = Characters[index].Avatar;
        CharacterDescription.text = Characters[index].Description.Replace("  Special", $".{Environment.NewLine}{Environment.NewLine}Special");
        CharacterName.text = Characters[index].Name;
        StartButton.enabled = CharacterName.text.ToLower() != "Knight_Artorias".ToLower();
    }

    public void LeftArrowPress() //this function will be used on our Exit button
    {
        characterIndex = (characterIndex + 1) % Characters.Count();
        SetCurrentAvatar();
    }

    public void RightArrowPress() //this function will be used for our "NO" button in our Quit Menu
    {
        characterIndex = (characterIndex - 1) % Characters.Count();
        SetCurrentAvatar();
    }

    public void StartLevel() //this function will be used on our Play button
    {
        gameController.SendMessage("SetName", CharacterName.text);
        SceneManager.LoadScene(2);
    }
}
