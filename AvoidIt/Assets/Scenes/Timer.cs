using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // 타이머 시간을 표시해주는 text
    public Button startButton; // 타이머를 시작할 수 있는 임시 버튼

    private Stopwatch stopwatch; // 사용되는 stopwatch

    void Start()
    {
        stopwatch = new Stopwatch(); // 새로운 stopwatch 생성

        startButton.onClick.AddListener(StartTimer); // 임시 버튼 onClick시 StartTimer 실행
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatch.IsRunning)
        {
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds; // 경과시간 불러오기

            // 시간 포맷: 초.밀리초 (예: 3.25초)
            float seconds = elapsedMilliseconds / 1000f;
            timerText.text = $"{seconds:F2}"; // 경과시간 표시
        }
    }

    void StartTimer()
    {
        stopwatch.Reset(); 
        stopwatch.Start();
    }
}
