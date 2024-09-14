using TMPro;
using UnityEngine;

public class NumberFormatter : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private ClockHandsSetter clockHandsSetter;

    public void SetTime(int time) => SetText(time.ToString());

    private void Awake()
    {
        inputField.onEndEdit.AddListener(OnInputChange);
        clockHandsSetter.OnTimeChanged += SetText;
    }

    private void SetText(string text)
    {
        inputField.text = text;
        OnInputChange(text);
    }

    private void OnInputChange(string input)
    {
        if (int.TryParse(input, out int number))
        {
            if (number < 10)
            {
                inputField.text = "0" + number.ToString();
                inputField.caretPosition = inputField.text.Length;
            }
        }

        clockHandsSetter.SetAlarmHands(input);
    }
}
