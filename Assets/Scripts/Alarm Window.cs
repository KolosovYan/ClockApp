using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmWindow : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject alarmWindow;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject alarmClockWindow;

    private bool alarmClockState;

    public void EnableAlarmWindow()
    {
        alarmWindow.SetActive(true);

        if (alarmClockWindow.activeSelf)
        {
            alarmClockState = true;
            alarmClockWindow.SetActive(false);
        }

        audioSource.Play();
    }

    public void DisableAlarmWindow()
    {
        audioSource.Stop();
        if (alarmClockState)
            alarmClockWindow.SetActive(true);
        alarmWindow.SetActive(false);
    }
}
