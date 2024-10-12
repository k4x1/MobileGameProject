using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HudMenu : MonoBehaviour
{
    public Button pauseButton;
    bool pause = true;
    private void OnEnable()
    {
        pauseButton.onClick.AddListener(PauseManager.Instance.TogglePause);
        pauseButton.onClick.AddListener(TurnOnPauseMenu);
      
            
        

    }
    private void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();

     
    }
    void TurnOnPauseMenu()
    {
        UiManager.Instance.SetPauseMenu(pause);
        pause = !pause;
    }
}
