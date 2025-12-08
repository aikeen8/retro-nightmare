using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool alreadyTriggered = false;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void Update()
    {
        if (!alreadyTriggered && GameObject.FindGameObjectWithTag("Player") == null)
        {
            alreadyTriggered = true;

            if (scoreManager != null)
                scoreManager.StopScore();

            gameOverPanel.SetActive(true);

            if (AudioManager.instance != null)
                AudioManager.instance.PlayGameOverBGM();
        }
    }

    public void Retry()
    {
        Debug.Log("Reload Current Scene");

        // Attach event FIRST
        SceneManager.sceneLoaded += OnGameplayLoaded;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        // Attach event FIRST
        SceneManager.sceneLoaded += OnMenuLoaded;

        SceneManager.LoadScene("Main Menu");
    }

    // Called AFTER scene loads
    private void OnGameplayLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.instance.PlayGameplayBGM();
        SceneManager.sceneLoaded -= OnGameplayLoaded; // Remove event
    }

    private void OnMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.instance.PlayMainMenuBGM();
        SceneManager.sceneLoaded -= OnMenuLoaded; // Remove event
    }
}
