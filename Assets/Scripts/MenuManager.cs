using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour


{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private string menuSceneName = "MenuScene";
    private bool isInMenu = false;
    // If you want, assign your player in the inspector, or find by tag
    [SerializeField] private GameObject player;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player has the "Player" tag!
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInMenu)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    void OpenMenu()
    {
        SceneManager.LoadSceneAsync(menuSceneName, LoadSceneMode.Additive).completed += (op) =>
        {
            SetPlayerCanvasActive(false); // Disable player UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isInMenu = true;
        };
    }

    void CloseMenu()
    {
        SceneManager.UnloadSceneAsync(menuSceneName).completed += (op) =>
        {
            SetPlayerCanvasActive(true); // Enable player UI
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isInMenu = false;
        };
    }

    private void SetPlayerCanvasActive(bool active)
    {
        if (player == null) return;

        Canvas[] canvases = player.GetComponentsInChildren<Canvas>(true);
        foreach (var canvas in canvases)
        {
            canvas.gameObject.SetActive(active);
        }
    }
    
    
    
}