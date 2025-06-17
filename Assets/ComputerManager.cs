using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    private static ComputerManager _current;
    public static ComputerManager Current { get { return _current; } }
    
    [SerializeField] private computer computerScript; // assign in inspector
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(this.gameObject);
        } else {
            _current = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject[] ComputerWindows;
    // public void OpenComputerWindow(int NumberWindow)
    // {
    //     ComputerWindows[NumberWindow].SetActive(true);
    //     SetWindowIndexAsCurrentWindow(NumberWindow);
    //   
    // }
    
    public void OpenComputerWindow(int NumberWindow)
    {
        if (NumberWindow < 0 || NumberWindow >= ComputerWindows.Length)
        {
            Debug.LogWarning("Invalid window index.");
            return;
        }

        ComputerWindows[NumberWindow].SetActive(true);
        SetWindowIndexAsCurrentWindow(NumberWindow);

   
        if (NumberWindow >= ComputerWindows.Length - 3)
        {
            if (computerScript != null)
            {
                computerScript.StartChore();
                computerScript.ActivateCameras();
            }
            else
            {
                Debug.LogWarning("No computer script assigned in ComputerManager.");
            }
        }
    }
    
    
    

    public void CloseWindow(int NumberWindow)
    {
        ComputerWindows[NumberWindow].SetActive(false);
    }

    public void SetWindowIndexAsCurrentWindow(int NumberWindow)
    {
        ComputerWindows[NumberWindow].gameObject.transform.SetSiblingIndex(ComputerWindows.Length - 1);
    }
}
