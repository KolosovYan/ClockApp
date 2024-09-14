using UnityEngine;

public class ClockHandsSetter : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private Transform cashedTransform;
    [SerializeField] private SpriteRenderer cashedRenderer;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;


    [Header("Settings")]

    [SerializeField] private ClockHandType type;

    public bool TimeOverTwelve {  get; private set; }

    private delegate void HandSelected();
    private static event HandSelected OnHandSelected;

    public delegate void UpdateTimeText(string newTime);
    public event UpdateTimeText OnTimeChanged;

    private bool isHandSelected = false;
    private float angleStep;
    private float angle;
    private float rotationAngle;
    private int newHour;
    private int currentHour;

    public void SetTimeOverTwelve(bool state) => TimeOverTwelve = state;
    public void SetAlarmHands(string inputText)
    {
        int parsedValue = int.Parse(inputText);
        SetRotationAngle(parsedValue);
    }

    private enum ClockHandType
    {
        HourHand,
        MinuteHand
    }

    private void Awake()
    {
        OnHandSelected += DeselectHand;
        angleStep = type == ClockHandType.HourHand ? 30f : 6f;
    }

    private void OnMouseDown()
    {
        SelectHand();
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)cashedTransform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotationAngle = angle - 90f;
        newHour = (int)Mathf.Round(rotationAngle / -angleStep);

        if (type == ClockHandType.HourHand)
        {
            cashedTransform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            newHour = (int)Mathf.Repeat(newHour, 12);
        }

        else
            newHour = (int)Mathf.Repeat(newHour, 60);

        MoveHand();
    }

    private void MoveHand()
    {
        if (newHour != currentHour)
        {
            currentHour = newHour;

            string newTime;

            if (type == ClockHandType.HourHand)
            {
                int tempTime = currentHour;

                if (!TimeOverTwelve && tempTime == 0)
                    tempTime = 12;

                if (TimeOverTwelve)
                    tempTime += 12;

                if (TimeOverTwelve && tempTime == 12)
                    tempTime = 0;

                newTime = tempTime.ToString();
            }

            else
            {
                SetAngleByHour();
                newTime = currentHour.ToString();
            }

            OnTimeChanged?.Invoke(newTime);
        }
    }

    private void OnMouseUp()
    {
        OnHandSelected?.Invoke();

        if (type == ClockHandType.HourHand)
            SetAngleByHour();

    }

    private void SetAngleByHour()
    {
        float targetAngle = currentHour * -angleStep;
        cashedTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    private void SelectHand()
    {
        OnHandSelected?.Invoke();
        cashedRenderer.material = selectedMaterial;
        isHandSelected = true;
    }

    private void DeselectHand()
    {
        if (isHandSelected)
            cashedRenderer.material = defaultMaterial;
    }

    private void SetRotationAngle(int value)
    {
        float angle = 0;

        switch (type)
        {
            case (ClockHandType.HourHand):
                if (value > 12)
                    TimeOverTwelve = true;
                angle = (value % 12 / 12f) * 360f;
                break;
            case (ClockHandType.MinuteHand):
                angle = (value / 60f) * 360f;
                break;
        }

        cashedTransform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
