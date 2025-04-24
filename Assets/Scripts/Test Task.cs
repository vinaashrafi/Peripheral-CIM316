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

  /* public void OnGUI()
   {
      if (GUILayout.Button("Task update"))
      {
         taskStarted = true;
         taskCounter++;
      }
   }*/
  public void Update()
  {
     uiTaskText.text = taskText;
     taskText = ($"{gameObject.name} 3/{taskCounter}");
  }
}
