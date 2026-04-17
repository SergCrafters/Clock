using UnityEngine;

public class RotaterHands : MonoBehaviour
{
    public const float DEVIATION_SECONDSHAND_FOR_SECONDS = 6f;
    //public const float DEVIATION_MINUTESHAND_FOR_SECONDS = 0.085f;
    //public const float DEVIATION_MINUTESHAND_FOR_SECONDS = 0.095f;
    //public const float DEVIATION_MINUTESHAND_FOR_MINUTES = 5.88f;
    //public const float DEVIATION_MINUTESHAND_FOR_SECONDS = 0.1f;
    public const float DEVIATION_MINUTESHAND_FOR_SECONDS = 0.09f;
    //public const float DEVIATION_MINUTESHAND_FOR_MINUTES = 5.98f;
    public const float DEVIATION_MINUTESHAND_FOR_MINUTES = 6f;
    public const float DEVIATION_HOURSHAND_FOR_MINUTES = 0.5f;
    public const float DEVIATION_HOURSHAND_FOR_HOURS = 30f;

    [SerializeField] private Transform _secondsHand;
    [SerializeField] private Transform _minutesHand;
    [SerializeField] private Transform _hoursHand;

    public void CalculateAngle(float seconds, float minutes, float hours)
    {
        float secondsAngle = seconds * DEVIATION_SECONDSHAND_FOR_SECONDS;
        float minutesAngle = minutes * DEVIATION_MINUTESHAND_FOR_MINUTES;
        float hoursAngle = (hours % 12) * DEVIATION_HOURSHAND_FOR_HOURS;

        RotationHands(secondsAngle, minutesAngle, hoursAngle);
    }

    public void CalculateAngle(float totalSeconds)
    {
        float seconds = totalSeconds % 60;
        float minutes = (totalSeconds / 60) % 60;
        float hours = (totalSeconds / 3600) % 12;

        float secondsAngle = seconds * DEVIATION_SECONDSHAND_FOR_SECONDS;
        float minutesAngle = minutes * DEVIATION_MINUTESHAND_FOR_MINUTES;
        float hoursAngle = hours * DEVIATION_HOURSHAND_FOR_HOURS;

        RotationHands(secondsAngle, minutesAngle, hoursAngle);

        Debug.Log($"Time: {hours:F2}:{minutes:F2}:{seconds:F2}");
        Debug.Log($"Angles: sec={seconds * 6}, min={minutes * 5.98f + seconds * 0.09f}, hour={hours * 30 + minutes * 0.5f}");
    }

    private void RotationHands(float secondsAngle, float minutesAngle, float hoursAngle)
    {
        _secondsHand.localRotation = Quaternion.Euler(0, 0, -secondsAngle);
        _minutesHand.localRotation = Quaternion.Euler(0, 0, -minutesAngle);
        _hoursHand.localRotation = Quaternion.Euler(0, 0, -hoursAngle);
    }
}

