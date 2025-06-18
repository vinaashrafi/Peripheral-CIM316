using System;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEditor.Rendering;

public class UIPopUpManager : PopUpBase
{
    public void Awake()
    {
       
    }

    public void Update()
    {
        if (player.crosshair = parentRB.detectCollisions)
        {
            popUpText.enabled = true;
        }
    }
}
