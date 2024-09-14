using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmNotify : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Animator cashedAnimator;

    private void Awake()
    {
        AlarmClock.AlarmSettedNotify += ShowNotify;
    }

    private void ShowNotify() => cashedAnimator.SetTrigger("AlarmSetted");
}
