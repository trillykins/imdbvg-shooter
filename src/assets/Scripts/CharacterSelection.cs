using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class CharacterSelection : MonoBehaviour
{
    private int index;
    private GameObject gameController;

    public List<Character> Characters;
    public Canvas SelectionMenu;
    public Image Avatar;
    public Text CharacterName;
    public Text CharacterDescription;
    public Button StartButton;

    private Character CurrentCharacter;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        SelectionMenu = SelectionMenu.GetComponent<Canvas>();

        index = UnityEngine.Random.Range(0, Characters.Count());
        SetCurrentAvatar();
    }

    private void SetCurrentAvatar(int i = 0)
    {
        index += i;
        if (index >= Characters.Count()) index = 0;
        if (index < 0) index = Characters.Count() - 1;
        Avatar.sprite = Characters[index].Avatar;
        CharacterDescription.text = Characters[index].Description.Replace("  Special", $".{Environment.NewLine}{Environment.NewLine}Special");
        CharacterName.text = Characters[index].Name;
        StartButton.enabled = CharacterName.text.ToLower() != "Knight_Artorias".ToLower();
        Debug.Log($"Character selection: {index}");
    }

    public void LeftArrowPress() //this function will be used on our Exit button
    {
        SetCurrentAvatar(-1);
    }

    public void RightArrowPress() //this function will be used for our "NO" button in our Quit Menu
    {
        SetCurrentAvatar(1);
    }

    public void StartLevel() //this function will be used on our Play button
    {
        gameController.SendMessage("SetName", CharacterName.text);
        SceneManager.LoadScene(2);
    }
}
