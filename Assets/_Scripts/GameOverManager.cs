using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("UI panel (or canvas child) that shows the Game Over screen.")]
    public GameObject gameOverScreen;

    [Tooltip("Reference to the player's HP script. If left empty, it will be found at runtime.")]
    public PlayerHP playerHP;

    [Header("Behaviour")]
    [Tooltip("Pause the game (Time.timeScale = 0) when Game Over.")]
    public bool pauseOnGameOver = true;

    private bool isGameOver = false;

    private void Start()
    {
        // Hide Game Over UI at start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Auto-find PlayerHP if not assigned
        if (playerHP == null)
        {
            playerHP = FindObjectOfType<PlayerHP>();
        }
    }

    private void Update()
    {
        if (isGameOver)
            return;

        // If playerHP reference is gone, assume the player has died
        if (playerHP == null)
        {
            TriggerGameOver();
            return;
        }

        // Optional: If you want to react before the player is Destroyed,
        // you can also check the HP value:
        // if (playerHP.HP <= 0f)
        // {
        //     TriggerGameOver();
        // }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        if (pauseOnGameOver)
        {
            Time.timeScale = 0f;
        }

        Debug.Log("Game Over");
    }

    // Called by your UI button
    public void RestartGame()
    {
        // Resume time
        Time.timeScale = 1f;

        // Reload current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}

