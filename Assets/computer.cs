using UnityEngine;
using UnityEngine.UI;

public class computer : ChoreBase
{
    [SerializeField] private Camera[] cctvCameras; // Assign 3 cameras in Inspector
    private int currentCameraIndex = 0;
    private bool isViewingCCTV = false;
    private Canvas[] allCanvases;

    public override void StartChore()
    {
        base.StartChore();

        if (cctvCameras.Length == 0)
        {
            Debug.LogWarning("No CCTV cameras assigned!");
            return;
        }
        
        
        // 🔻 Disable all canvases (except world-space ones if needed)
        allCanvases = FindObjectsOfType<Canvas>();
        foreach (Canvas canvas in allCanvases)
        {
            canvas.enabled = false;
        }
        
        isViewingCCTV = true;
        currentCameraIndex = 0;
        ActivateCamera(currentCameraIndex);
        
        
        
        
        Debug.Log("Started viewing CCTV.");
    }

    public override void StopChore()
    {
        base.StopChore();
        ExitCCTVView();
    }

    public override void CompleteChore()
    {
        base.CompleteChore();
        ExitCCTVView();
    }

    void Update()
    {
        if (!isViewingCCTV) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SwitchCamera(1);
        }
        else if (scroll < 0f)
        {
            SwitchCamera(-1);
        }
    }

    private void SwitchCamera(int direction)
    {
        cctvCameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex += direction;
        if (currentCameraIndex >= cctvCameras.Length) currentCameraIndex = 0;
        if (currentCameraIndex < 0) currentCameraIndex = cctvCameras.Length - 1;

        ActivateCamera(currentCameraIndex);
        Debug.Log("Switched to CCTV camera: " + currentCameraIndex);
    }

    private void ActivateCamera(int index)
    {
        for (int i = 0; i < cctvCameras.Length; i++)
        {
            cctvCameras[i].gameObject.SetActive(i == index);
        }
    }

    private void ExitCCTVView()
    {
        isViewingCCTV = false;
        foreach (var cam in cctvCameras)
        {
            cam.gameObject.SetActive(false);
        }
        Debug.Log("Exited CCTV view.");
        
        
        // 🔺 Re-enable all canvases
        if (allCanvases != null)
        {
            foreach (Canvas canvas in allCanvases)
            {
                canvas.enabled = true;
            }
        }
    }
}