using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    private float currentTime;
    private bool isRunning = true;
    private TextMeshProUGUI stopwatchText;

    private void Start()
    {
        currentTime = 0f;
        stopwatchText = GetComponent<TextMeshProUGUI>();
        UpdateStopwatchText();
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            UpdateStopwatchText();
        }
    }

    private void UpdateStopwatchText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        //int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 100);
        string stopwatchString = string.Format("{0:00}:{1:00}", minutes, seconds);
        stopwatchText.text = stopwatchString;
    }

    public void PauseStopwatch()
    {
        //스톱워치 중지
        isRunning = false;
    }
}
