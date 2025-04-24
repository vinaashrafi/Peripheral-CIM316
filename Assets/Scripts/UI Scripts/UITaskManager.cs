using System;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class UITaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskTexts;
    public TaskBase[] tasksList;
    public TaskBase tasks;

    public delegate void TextSpawner(string taskText);
  
    public event TextSpawner TextSpawner_Event;

    public void TextTaskSpawner()
    {
        
    }
}
