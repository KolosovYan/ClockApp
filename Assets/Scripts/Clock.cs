using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private TextMeshProUGUI clock;
    [SerializeField] private AlarmClock alarmClock;
    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private Transform secondHand;

    private DateTime currentTime;

    public void SetTime(DateTime tempTime)
    {
        StopAllCoroutines();
        currentTime = tempTime;
        StartCoroutine(UpdateClock());
    }

    public int GetHour()
    {
        return currentTime.Hour;
    }

    public int GetMinute()
    {
        return currentTime.Minute;
    }

    private IEnumerator UpdateClock()
    {
        while (true)
        {
            clock.text = currentTime.ToString("HH:mm:ss");
            UpdateClockHands();

            alarmClock.CheckAlarm(currentTime);

            currentTime = currentTime.Add(TimeSpan.FromSeconds(1));

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateClockHands()
    {
        float secondsAngle = (currentTime.Second / 60f) * 360f;
        float minutesAngle = (currentTime.Minute / 60f) * 360f + (currentTime.Second / 60f) * 6f;
        float hoursAngle = (currentTime.Hour % 12 / 12f) * 360f + (currentTime.Minute / 60f) * 30f;

        secondHand.rotation = Quaternion.Euler(0, 0, -secondsAngle);
        minuteHand.rotation = Quaternion.Euler(0, 0, -minutesAngle);
        hourHand.rotation = Quaternion.Euler(0, 0, -hoursAngle);
    }
}
