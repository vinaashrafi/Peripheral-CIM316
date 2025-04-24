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

  
  
 
  
  


  public void OnEnable()
  {
    taskManager.TextSpawner_Event += TaskManagerOnTextSpawner_Event;
  }

  private void TaskManagerOnTextSpawner_Event(string tasktext)
  {
    throw new NotImplementedException();
  }


  public void OnDisable()
  {
    taskManager.TextSpawner_Event -= TaskManagerOnTextSpawner_Event;
  }
}
