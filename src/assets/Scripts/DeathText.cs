using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathText : MonoBehaviour
{

    public Text text;

    void Start()
    {
        text.text = GameController.Character?.DeathText ?? "Debugging scene; no character specific data!";
    }
}
