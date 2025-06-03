using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Button startButton;

    private Stopwatch stopwatch;

    void Start()
    {
        stopwatch = new Stopwatch();

        startButton.onClick.AddListener(StartTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatch.IsRunning)
        {
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // 시간 포맷: 초.밀리초 (예: 3.254초)
            float seconds = elapsedMilliseconds / 1000f;
            timerText.text = $"{seconds:F2}";
        }
    }

    void StartTimer()
    {
        stopwatch.Reset();
        stopwatch.Start();
    }
}
