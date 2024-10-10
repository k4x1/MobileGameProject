using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum InputMode
{
    JOYSTICK,
    RELATIVE_TOUCH,
    GYROSCOPE
}


public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

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

    public void SwitchInputMode(InputMode mode)
    {
        InputManager.Instance.SetInputMode(mode);
    }

    public void ResetGyroscope()
    {
        InputManager.Instance.ResetGyroscope();
    }
}