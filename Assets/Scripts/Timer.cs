using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timerDuration = 5f; 
    [SerializeField] private TMP_Text timerText;

    private float timer;
    private bool timerRunning = false;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (timerRunning)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timer <= 0f)
            {
                timerRunning = false;
                TimerDone();
            }
        }
    }

    public void StartTimer()
    {
        timer = timerDuration;
        timerRunning = true;
        UpdateTimerDisplay(); 
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Max(0, timer).ToString("F2"); 
        }
    }

    private void TimerDone()
    {
        Debug.Log("timer done");
    }
}
