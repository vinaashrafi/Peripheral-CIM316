using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TaskBase : MonoBehaviour
{
  public bool taskStarted; 
  public bool taskCompleted;

  public int taskCounter;
  
  public UITaskManager taskManager;
  
  public string taskText;

  public TextMeshProUGUI uiTaskText;

  public delegate void TextSpawner(GameObject task);
  
  public event TextSpawner TextSpawner_Event;

  public void OnEnable()
  {
      TextSpawner_Event?.Invoke(gameObject);
  }
}
