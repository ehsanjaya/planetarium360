using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Menu UIs")]
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject controlsMenuUI;
    [Header("Game Is Pause")]
    public bool gameIsPause = false;
    [Header("Control Player Script")]
    public ControlPlayer controlPlayer = new ControlPlayer();
    [Header("Camera Zoom Script")]
    public CameraZoom cameraZoom = new CameraZoom();
    [Header("Key Controls Desktop")]
    public KeyBindingControl pauseKeyBindDesktop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameIsPause = false;
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKeyBindDesktop.keyCode))
        {
            if (gameIsPause) Resume();
            else OnPauseMenu();
        }
    }

    public void Resume()
    {
        controlPlayer.mouseVisible = false;
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;

        cameraZoom.zoom = 60f;
    }

    public void OnPauseMenu()
    {
        controlPlayer.mouseVisible = true;
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void OnSettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        controlsMenuUI.SetActive(false);
    }

    public void OnControlsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        controlsMenuUI.SetActive(true);
    }

    public void Exit()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        controlsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
