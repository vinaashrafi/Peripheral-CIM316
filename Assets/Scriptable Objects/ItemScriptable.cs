using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemScriptable : ScriptableObject
{
    public Sprite image;
    public string nameOfItem;
}
