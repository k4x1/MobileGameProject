using TMPro;
using Unity.VisualScripting;
using UnityEngine;

enum InputMode
{
    JOYSTICK,
}
public class UiManager : MonoBehaviour
{

    public static UiManager Instance { get; private set; }

    private void Start()
    {
      
    }
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

 
}
