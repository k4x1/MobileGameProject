using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreenMenu : MonoBehaviour
{
    public Button RetryButton;
    public Button MainMenuButton;

    private void OnEnable()
    {
  
        MainMenuButton.onClick.AddListener(GameManager.Instance.MainMenu);

    }
    private void OnDisable()
    {
        MainMenuButton.onClick.RemoveAllListeners();


    }
   
}
