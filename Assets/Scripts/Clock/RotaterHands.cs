using System;
using UnityEngine;

public class RotaterHands : MonoBehaviour
{
    [SerializeField] private Transform _secondsHand;
    [SerializeField] private Transform _minutesHand;
    [SerializeField] private Transform _hoursHand;

    public void SetTime(TimeSpan time)
    {
        float seconds = time.Seconds + time.Milliseconds / ClockConstants.MILLISECONDS_PER_SECOND;
        float minutes = time.Minutes + seconds / ClockConstants.SECONDS_PER_MINUTE;
        float hours = (time.Hours % ClockConstants.HOURS_ON_ANALOG_CLOCK) + minutes / ClockConstants.MINUTES_PER_HOUR;

        float secondsAngle = seconds * ClockConstants.DEGREES_PER_SECOND_AND_MINUTE;
        float minutesAngle = minutes * ClockConstants.DEGREES_PER_SECOND_AND_MINUTE;
        float hoursAngle = hours * ClockConstants.DEGREES_PER_HOUR;

        _secondsHand.localRotation = Quaternion.Euler(0f, 0f, -secondsAngle);
        _minutesHand.localRotation = Quaternion.Euler(0f, 0f, -minutesAngle);
        _hoursHand.localRotation = Quaternion.Euler(0f, 0f, -hoursAngle);
    }
}