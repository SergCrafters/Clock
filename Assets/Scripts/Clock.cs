using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private float _seconds;
    [SerializeField] private float _minutes;
    [SerializeField] private float _hours;

    private TimeSpan _currentTime = new(6, 58, 45);

    private RotaterHands _rotater;

    private void Awake()
    {
        _rotater = GetComponent<RotaterHands>();
    }

    void Update()
    {
        _currentTime += TimeSpan.FromSeconds(Time.deltaTime);

        float totalSeconds = (float)_currentTime.TotalSeconds;
        float totalMinutes = (float)_currentTime.TotalMinutes;
        float totalHours = (float)_currentTime.TotalHours;

        _rotater.CalculateAngle(totalSeconds % 60, totalMinutes % 60, totalHours % 24);

    }
}
