using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool won = false;
    public bool reset = false;
    public bool gameStarted = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetLevel() {
        reset = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        gameStarted = true;
    }
    public void MainMenu()
    {
        PauseManager.Instance.SetPaused(false);
        SceneManager.LoadScene("MainMenu");
        gameStarted = false;
    }
}
