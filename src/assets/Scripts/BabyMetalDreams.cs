using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BabyMetalDreams : MonoBehaviour
{
    public Image baby;
    private Color Colour;

    void Start()
    {
        Colour = baby.color;
    }

    void Update()
    {
        if ((GameController.Character?.Name ?? "").Equals("Space_Wolf27"))
        {
            Colour = baby.color;
            Colour.a = Mathf.PingPong(Time.time * 0.05f, .2f);
            baby.color = Colour;
        }
    }
}
