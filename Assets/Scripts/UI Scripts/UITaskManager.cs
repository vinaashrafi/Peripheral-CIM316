using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class UITaskManager : MonoBehaviour
{
    public TextMeshProUGUI taskTexts;
    public List<GameObject> taskList;
    public TaskBase tasks;

    public void OnEnable()
    {
        tasks.TextSpawner_Event += TaskManagerOnTextSpawner_Event;
    }

    private void TaskManagerOnTextSpawner_Event(GameObject uiText)
    {
       uiText.SetActive(true);
    }


    public void OnDisable()
    {
        tasks.TextSpawner_Event -= TaskManagerOnTextSpawner_Event;
    }
}
