using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class computer : ChoreBase
{
    [SerializeField] private Camera[] cctvCameras;
    [SerializeField] private Canvas computerCanvas;

    private int currentCameraIndex = 0;
    private bool isViewingCCTV = false;
    private FPController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<FPController>();
    }

    public override void StartChore()
    {
        base.StartChore();

        if (computerCanvas != null)
            computerCanvas.gameObject.SetActive(true);

        if (playerController != null)
        {
            playerController.DisableInput();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Debug.Log("Started computer chore. Canvas enabled.");
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

    public void OnCameraSelected(int index)
    {
        if (index < 0 || index >= cctvCameras.Length)
        {
            Debug.LogWarning("Invalid camera index.");
            return;
        }

        currentCameraIndex = index;
        isViewingCCTV = true;

        if (computerCanvas != null)
            computerCanvas.enabled = false;

        ActivateCamera(currentCameraIndex);
        Debug.Log("Viewing camera index: " + index);
    }

    public void ExitCCTVView()
    {
        isViewingCCTV = false;

        foreach (Camera cam in cctvCameras)
            cam.gameObject.SetActive(false);

        if (computerCanvas != null)
            computerCanvas.gameObject.SetActive(false);

        if (playerController != null)
        {
            playerController.EnableInput();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Debug.Log("Exited CCTV view.");
    }

    private void SwitchCamera(int direction)
    {
        cctvCameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + direction + cctvCameras.Length) % cctvCameras.Length;
        ActivateCamera(currentCameraIndex);
    }

    private void ActivateCamera(int index)
    {
        for (int i = 0; i < cctvCameras.Length; i++)
            cctvCameras[i].gameObject.SetActive(i == index);
    }
}