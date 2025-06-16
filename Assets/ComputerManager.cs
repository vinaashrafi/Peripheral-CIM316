using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    private static ComputerManager _current;
    public static ComputerManager Current { get { return _current; } }

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
    public void OpenComputerWindow(int NumberWindow)
    {
        ComputerWindows[NumberWindow].SetActive(true);
    }
}
