using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum InputMode
{
    JOYSTICK,
    RELATIVE_TOUCH,
    GYROSCOPE
}


public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject resetGyroButton;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          /*  DontDestroyOnLoad(gameObject);*/
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SwitchInputMode(InputManager.Instance.CurrentInputMode);
    }
    public void SwitchInputMode(InputMode mode)
    {
        Debug.Log("switched");
        joystick.SetActive(InputMode.JOYSTICK == mode);
        if (InputMode.GYROSCOPE == mode)
        {
            resetGyroButton.SetActive(true);
            resetGyroButton.GetComponentInChildren<Button>().onClick.AddListener(ResetGyroscope);
        }
        else
        {
            resetGyroButton.SetActive(false);
            resetGyroButton.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        }
        InputManager.Instance.SetInputMode(mode);
    }

    public void ResetGyroscope()
    {
        InputManager.Instance.ResetGyroscope();
    }
    public void SetPauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

}