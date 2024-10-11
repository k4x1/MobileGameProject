using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InputModeDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown inputModeDropdown;
    [SerializeField] private LevelMovement levelMovement;
    [SerializeField] private GameObject resetGyroscopeButton;

    private void Start()
    {
        if (inputModeDropdown == null)
        {
            inputModeDropdown = GetComponent<TMP_Dropdown>();
        }

        if (inputModeDropdown != null)
        {
            inputModeDropdown.ClearOptions();

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("Joystick"),
                new TMP_Dropdown.OptionData("Relative Touch"),
                new TMP_Dropdown.OptionData("Gyroscope")
            };
            inputModeDropdown.AddOptions(options);

            inputModeDropdown.value = (int)InputManager.Instance.CurrentInputMode;

            inputModeDropdown.onValueChanged.AddListener(OnInputModeChanged);
        }
        else
        {
            Debug.LogError("dropdown not assigned or found");
        }

        UpdateResetGyroscopeButtonVisibility();
    }

    private void OnInputModeChanged(int index)
    {
        InputMode newMode = (InputMode)index;
        if (GameManager.Instance.gameStarted)
        {
            UiManager.Instance.SwitchInputMode(newMode);
        }
        else { 
            InputManager.Instance.SetInputMode(newMode);
        }
        UpdateResetGyroscopeButtonVisibility();
    }

    public void ResetGyroscope()
    {
        if (levelMovement != null)
        {
            levelMovement.ResetGyroscope();
        }
        else
        {
            InputManager.Instance.ResetGyroscope();
        }
    }

    private void UpdateResetGyroscopeButtonVisibility()
    {
        if (resetGyroscopeButton != null)
        {
            resetGyroscopeButton.SetActive(InputManager.Instance.CurrentInputMode == InputMode.GYROSCOPE);
        }
    }
}
