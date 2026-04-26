using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private float _seconds;
    [SerializeField] private float _minutes;
    [SerializeField] private float _hours;

    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private RequesterTime _requesterTime;

    private float _maxHours = 24;
    private float _maxMinutes = 60;
    private float _maxSeconds = 60;

    private TimeSpan _currentTime = new(0, 0, 0);

    private RotaterHands _rotater;

    private void Awake()
    {
        _rotater = GetComponent<RotaterHands>();
        _requesterTime = GetComponent<RequesterTime>();

        _requesterTime.GetServerTime((DateTime serverTime) =>
        {
            _currentTime = new TimeSpan(serverTime.Hour, serverTime.Minute, serverTime.Second);
        });
        //_requesterTime.GetServerTime((DateTime serverTime) => _currentTime = new TimeSpan(serverTime.Hour, serverTime.Minute, serverTime.Second));
    }

    private void Update()
    {
        _currentTime += TimeSpan.FromSeconds(Time.deltaTime);

        float totalSeconds = (float)_currentTime.TotalSeconds;
        float totalMinutes = (float)_currentTime.TotalMinutes;
        float totalHours = (float)_currentTime.TotalHours;

        _rotater.CalculateAngle(totalSeconds % _maxSeconds, totalMinutes % _maxMinutes, totalHours % _maxHours);
        _timePanel.ShowTime(_currentTime.Hours, _currentTime.Minutes, _currentTime.Seconds);
    }

    public void EditTime(int hour, int minute, int second)
    {
        _currentTime = new(hour, minute, second);
    }

    public TimeSpan GetCurrentTime()
    {
        return _currentTime;
    }
}
