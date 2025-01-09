using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject pauseFromPlaceModePanel;

    [SerializeField]
    GameObject pauseFromPlayModePanel;

    [SerializeField]
    GameObject placeModePanel;

    [SerializeField]
    GameObject playModePanel;

    [SerializeField]
    GameObject winModePanel;

    [SerializeField]
    GameObject gameOverModePanel;

    [SerializeField]
    PlaceOnIndicator placeModeScript;

    [SerializeField]
    ShootController playModeScript;

    public void StartPlayMode()
    {
        // Disable place mode
        placeModePanel.SetActive(false);
        placeModeScript.enabled = false;

        // Enable play mode
        playModePanel.SetActive(true);
        playModeScript.enabled = true;
    }

    public void PauseGameFromPlace()
    {
        // Enable pause panel
        pauseFromPlaceModePanel.SetActive(true);

        // Disable place mode
        placeModePanel.SetActive(false);
        placeModeScript.enabled = false;

        // Handle pause logic
        Time.timeScale = 0;
    }

    public void PauseGameFromPlay()
    {
        // Enable pause panel
        pauseFromPlayModePanel.SetActive(true);

        // Disable play mode
        playModePanel.SetActive(false);
        playModeScript.enabled = false;

        // Handle pause logic
        Time.timeScale = 0;
    }

    public void ResumeGameFromPlace()
    {
        // Disable pause panel
        pauseFromPlaceModePanel.SetActive(false);

        // Enable place mode
        placeModePanel.SetActive(true);
        placeModeScript.enabled = true;

        // Handle resume logic
        Time.timeScale = 1;
    }

    public void ResumeGameFromPlay()
    {
        // Disable pause panel
        pauseFromPlayModePanel.SetActive(false);

        // Enable play mode
        playModePanel.SetActive(true);
        playModeScript.enabled = true;

        // Handle resume logic
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        // Disable play mode
        playModePanel.SetActive(false);
        playModeScript.enabled = false;

        // Enable win panel
        winModePanel.SetActive(true);

        // Handle pause logic
        Time.timeScale = 0;
    }

    public void LoseGame()
    {
        // Disable play mode
        playModePanel.SetActive(false);
        playModeScript.enabled = false;

        // Enable lose panel
        gameOverModePanel.SetActive(true);

        // Handle pause logic
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
