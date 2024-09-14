using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmClock : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Clock clock;
    [SerializeField] private NumberFormatter hoursNumberFormatter;
    [SerializeField] private NumberFormatter minutesNumberFormatter;
    [SerializeField] private AlarmWindow alarmWindow;

    public delegate void AlarmSetted();
    public static event AlarmSetted AlarmSettedNotify;

    private DateTime alarmTime;
    private bool isAlarmSet = false;

    public void SetAlarm(DateTime time)
    {
        alarmTime = time;
        isAlarmSet = true;
        AlarmSettedNotify?.Invoke();
    }

    public void CheckAlarm(DateTime currentTime)
    {
        if (isAlarmSet && currentTime >= alarmTime)
        {
            TriggerAlarm();
            isAlarmSet = false;
        }
    }

    private void OnEnable()
    {
        hoursNumberFormatter.SetTime(clock.GetHour());
        minutesNumberFormatter.SetTime(clock.GetMinute());
    }

    private void TriggerAlarm()
    {
        alarmWindow.EnableAlarmWindow();
    }
}
