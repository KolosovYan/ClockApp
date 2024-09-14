using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWindowButton : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject windowToEnable;
    [SerializeField] private GameObject underline;

    [SerializeField] private bool isWindowActiveByDefault;

    private delegate void WindowChanged();
    private static event WindowChanged OnWindowChanged;

    private bool isWindowActive = false;

    public void ButtonPressed()
    {
        if (!isWindowActive)
        {
            OnWindowChanged?.Invoke();
            EnableWindow();
        }
    }

    private void EnableWindow()
    {
        windowToEnable.SetActive(true);
        underline.SetActive(true);
        isWindowActive = true;
    }

    private void DisableWindow()
    {
        if (isWindowActive)
        {
            windowToEnable.SetActive(false);
            underline.SetActive(false);
            isWindowActive = false;
        }
    }

    private void Awake()
    {
        isWindowActive = isWindowActiveByDefault;
        OnWindowChanged += DisableWindow;

        if (isWindowActive)
            EnableWindow();
    }
}
