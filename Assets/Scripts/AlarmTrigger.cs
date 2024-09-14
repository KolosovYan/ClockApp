using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private ClockHandsSetter hourHand;

    private static AlarmTrigger firstFixed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HourHand"))
        {
            if (firstFixed == null)
                firstFixed = this;
        }

        if (this != firstFixed)
        {
            hourHand.SetTimeOverTwelve(!hourHand.TimeOverTwelve);
            firstFixed = null;
        }
    }
}
