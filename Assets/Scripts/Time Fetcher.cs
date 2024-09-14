using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class TimeFetcher : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Clock clock;
    [SerializeField] private string masterURL;
    [SerializeField] private string additionalURL;

    [Header("Settings")]

    [SerializeField] private float delayInMinutes;

    private DateTime utcTime;
    private WaitForSeconds updateTimeDelay;

    [System.Serializable]
    public class TimeResponse
    {
        public string datetime;
        public string dateTime;
    }

    private void Start()
    {
        CalculacteDelay();
        StartCoroutine(GetTime(masterURL));
    }

    private void CalculacteDelay()
    {
        updateTimeDelay = new WaitForSeconds(delayInMinutes * 60f);
    }

    private IEnumerator GetTime(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            StartCoroutine(GetTime(additionalURL));
            yield break;
        }

        string jsonResponse = request.downloadHandler.text;
        utcTime = ParseTime(jsonResponse, url);
        SetClockTime();
    }

    private DateTime ParseTime(string json, string url)
    {
        var timeData = JsonUtility.FromJson<TimeResponse>(json);
        string timeString = !string.IsNullOrEmpty(timeData.datetime) ? timeData.datetime : timeData.dateTime;

        if (url == additionalURL)
        {
            timeString += "+00:00";
        }

        return DateTime.Parse(timeString);
    }

    private void SetClockTime()
    {
        clock.SetTime(utcTime);
        StartCoroutine(WaitUntileUpdateTime());
    }

    private IEnumerator WaitUntileUpdateTime()
    {
        yield return updateTimeDelay;

        StartCoroutine(GetTime(masterURL));
    }
}
