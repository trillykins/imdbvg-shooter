using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Character
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public Sprite Avatar;
    [SerializeField]
    public string Description;
    [SerializeField]
    public string DeathText;
}
