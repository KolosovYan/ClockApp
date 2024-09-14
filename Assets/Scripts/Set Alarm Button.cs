using System;
using TMPro;
using UnityEngine;

public class SetAlarmButton : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private AlarmClock alarmClock;
    [SerializeField] private TMP_InputField hoursInputField;
    [SerializeField] private TMP_InputField minutesInputField;

    public void ButtonPressed()
    {
        alarmClock.SetAlarm(FormatTime());
    }

    private DateTime FormatTime()
    {
        DateTime nowDT = DateTime.Now;
        int hour = int.Parse(hoursInputField.text);
        int minute = int.Parse(minutesInputField.text);

        DateTime alarmTime = new DateTime(nowDT.Year, nowDT.Month, nowDT.Day, hour, minute, 0);

        if (alarmTime <= nowDT)
            alarmTime = alarmTime.AddDays(1);

        return alarmTime;
    }
}
