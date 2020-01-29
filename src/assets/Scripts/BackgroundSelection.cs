using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSelection : MonoBehaviour
{
    public Sprite[] Backgrounds;

    private SpriteRenderer _spriteRenderer;
    private Color[] _colours;

    public void SwitchBackground()
    {
        Debug.Log("Switching background!");
        _spriteRenderer.sprite = Backgrounds[Random.Range(0, Backgrounds.Length)];
        _spriteRenderer.color = _colours[Random.Range(0, Backgrounds.Length)];
        Debug.Log(_spriteRenderer.color);
    }

    void Start()
    {
        _colours = new Color[] { new Color(1, 1, 1, 1), new Color(0.4117f, 0.2119f, 0.2119f, 1) };
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SwitchBackground();
    }
}
