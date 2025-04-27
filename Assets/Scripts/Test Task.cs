using System;
using TMPro;
using UnityEditor;
using UnityEngine;


public class TestTask : TaskBase
{
   public void Awake()
   {
      //taskText = ($"{gameObject.name} 0/{taskCounter}");
      
     
   }
   
  public void Update()
  {
     if (uiTaskText == true)
     {
        uiTaskText.text = taskText;
        taskText = ($"{gameObject.name} 3/{taskCounter}");
     }
     
  }
}
