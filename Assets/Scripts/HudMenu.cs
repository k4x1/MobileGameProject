using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HudMenu : MonoBehaviour
{
    public Button pauseButton;

    private void OnEnable()
    {
        pauseButton.onClick.AddListener(PauseManager.Instance.TogglePause);

    }
    private void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();

     
    }
}
