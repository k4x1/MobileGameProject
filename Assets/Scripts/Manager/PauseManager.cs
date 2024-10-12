using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GameManager.Instance.gameStarted) { 
            CheckPaused();
        }
    }

    private void CheckPaused()
    {
        if (isPaused)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }

    public void SetPaused(bool pausedStatus)
    {
        isPaused = pausedStatus;
        CheckPaused();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        CheckPaused();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
       
        InputManager.Instance.DisablePlayerInput();
     
    }

     void UnPause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        InputManager.Instance.EnablePlayerInput();
    }
}