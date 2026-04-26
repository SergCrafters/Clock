using UnityEngine;

public class RotaterHands : MonoBehaviour
{
    public const float DEVIATION_SECONDSHAND = 6f;
    public const float DEVIATION_MINUTESHAND = 6f;
    public const float DEVIATION_HOURSHAND = 30f;

    [SerializeField] private Transform _secondsHand;
    [SerializeField] private Transform _minutesHand;
    [SerializeField] private Transform _hoursHand;

    private float _maxHoursOnClock = 12;

    public void CalculateAngle(float seconds, float minutes, float hours)
    {
        float secondsAngle = seconds * DEVIATION_SECONDSHAND;
        float minutesAngle = minutes * DEVIATION_MINUTESHAND;
        float hoursAngle = (hours % _maxHoursOnClock) * DEVIATION_HOURSHAND;

        RotationHands(secondsAngle, minutesAngle, hoursAngle);
    }

    private void RotationHands(float secondsAngle, float minutesAngle, float hoursAngle)
    {
        _secondsHand.localRotation = Quaternion.Euler(0, 0, -secondsAngle);
        _minutesHand.localRotation = Quaternion.Euler(0, 0, -minutesAngle);
        _hoursHand.localRotation = Quaternion.Euler(0, 0, -hoursAngle);
    }
}

