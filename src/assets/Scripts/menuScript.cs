using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public Canvas creditsMenu;
    public Button newGameText;
    public Button creditsText;

    void Start()
    {
        creditsMenu = creditsMenu.GetComponent<Canvas>();
        newGameText = newGameText.GetComponent<Button>();
        creditsText = creditsText.GetComponent<Button>();
        creditsMenu.enabled = false;
    }

    public void CreditsPress() //this function will be used on our Exit button
    {
        creditsMenu.enabled = true; //enable the Quit menu when we click the Exit button
        newGameText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
        creditsText.enabled = false;
    }

    public void ReturnPress() //this function will be used for our "NO" button in our Quit Menu
    {
        creditsMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
        newGameText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
        creditsText.enabled = true;
    }

    public void StartLevel() //this function will be used on our Play button
    {
        SceneManager.LoadSceneAsync(1);
    }
}