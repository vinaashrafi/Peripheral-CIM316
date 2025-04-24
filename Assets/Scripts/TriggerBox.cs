using System;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
            
    }

    private void OnTriggerEnter(Collider collision)
    {
       StartTask();
    }

    private void StartTask()
    {
        Debug.Log("TriggerBox Activated");
    }

    private void EndTask()
    {
        Debug.Log("TriggerBox Deactivated");
    }

    private void OnTriggerExit(Collider other)
    {
        EndTask();
    }
}
