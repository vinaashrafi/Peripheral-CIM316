using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class computer : ChoreBase
{
    [SerializeField] private Camera[] cctvCameras;
    [SerializeField] private Canvas computerCanvas;
    [SerializeField] private Canvas tutorialCanvas;

    private int currentCameraIndex = 0;
    private bool isViewingCCTV = false;
    private FPController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<FPController>();
    }

    // Enables the computer UI and disables player controls
    public void EnableComputerUI()
    {
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

    // Starts the chore and enables the computer UI
    public override void StartChore()
    {
        base.StartChore();
        EnableComputerUI();
    }

    // Exits the computer, disables cameras and UI, and re-enables player controls
    public void ExitComputer()
    {
        isViewingCCTV = false;

        foreach (var cam in cctvCameras)
            cam.gameObject.SetActive(false);

        if (computerCanvas != null)
            computerCanvas.gameObject.SetActive(false);

        if (playerController != null)
        {
            playerController.EnableInput();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Debug.Log("Exited Computer");
    }

    // Activates the CCTV cameras and disables all other canvases except tutorial UI
    public void ActivateCameras()
    {
        // Disable all canvases in the scene
        foreach (var canvas in FindObjectsOfType<Canvas>())
            canvas.gameObject.SetActive(false);

        // Enable tutorial canvas if assigned
        if (tutorialCanvas != null)
            tutorialCanvas.gameObject.SetActive(true);

        if (cctvCameras.Length == 0)
        {
            Debug.LogWarning("No CCTV cameras assigned!");
            return;
        }

        currentCameraIndex = 0;
        isViewingCCTV = true;
        ActivateCamera(currentCameraIndex);

        Debug.Log("Initial CCTV camera activated.");
    }

    private void Update()
    {
        if (!isViewingCCTV)
            return;

        HandleCameraCycleInput();
        HandleExitInput();
    }

    // Handles input for cycling through cameras
    private void HandleCameraCycleInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            SwitchCamera(1);
        else if (scroll < 0f)
            SwitchCamera(-1);

        if (Input.GetKeyDown(KeyCode.D))
            SwitchCamera(1);
        else if (Input.GetKeyDown(KeyCode.A))
            SwitchCamera(-1);
    }

    // Handles input for exiting the CCTV view
    private void HandleExitInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnableComputerUI();
            // Enable tutorial canvas if assigned
            if (tutorialCanvas != null)
                tutorialCanvas.gameObject.SetActive(false);
        }
    }

    // Switches to a different camera index
    private void SwitchCamera(int direction)
    {
        cctvCameras[currentCameraIndex].gameObject.SetActive(false);

        currentCameraIndex = (currentCameraIndex + direction + cctvCameras.Length) % cctvCameras.Length;
        ActivateCamera(currentCameraIndex);
    }

    // Activates the camera at the given index and disables the others
    private void ActivateCamera(int index)
    {
        for (int i = 0; i < cctvCameras.Length; i++)
            cctvCameras[i].gameObject.SetActive(i == index);
    }
}