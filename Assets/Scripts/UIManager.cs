using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject pauseMenu;

    public static bool isPaused;
    public static bool isGameOver;



    void Start()
    {
        pauseMenu.SetActive(false);
         
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();

            }
        }
    }


    public void PauseGame()
    {
        if (!isGameOver)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerDeath += EnableGameOverMenu;

    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerDeath -= EnableGameOverMenu;
        isGameOver = false;
    }
    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive (true);
        isGameOver = true;
    }


    public void ReturnToMainMenu()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel; // Poista tämä jos haluaa että pelaaja pausesta menuun palatessa aloittaa pelin alusta.
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void RestartLevel()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel;
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("Map");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        GameManager.manager.currentLevel = GameManager.manager.previousLevel; // Poista tämä jos haluaa että kuolemasta quit aloittaa pelin alusta.
        GameManager.manager.health = GameManager.manager.historyHealth;
        GameManager.manager.previousHealth = GameManager.manager.historyPreviousHealth;
        GameManager.manager.maxHealth = GameManager.manager.historyMaxHealth;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;

    }
}
