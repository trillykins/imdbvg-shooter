using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuScript : MonoBehaviour
{
    public Canvas CreditsMenu;
    public Button NewGameText;
    public Button CreditsText;

    private readonly List<Button> Buttons = new List<Button>();

    void Start()
    {
        Buttons.AddRange(new[] { NewGameText, CreditsText });
        CreditsMenu.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            Debug.Log("UP!");
            CreditsText.Select();
        }
        else if (Input.GetKeyDown("down"))
        {
            Debug.Log("DOWN!");
            NewGameText.Select();
        }
    }

    public void CreditsPress() //this function will be used on our Exit button
    {
        CreditsMenu.enabled = true; //enable the Quit menu when we click the Exit button
        NewGameText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
        CreditsText.enabled = false;
    }

    public void ReturnPress() //this function will be used for our "NO" button in our Quit Menu
    {
        CreditsMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
        NewGameText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
        CreditsText.enabled = true;
    }

    public void StartLevel() //this function will be used on our Play button
    {
        SceneManager.LoadSceneAsync(1);
    }
}