using System;
using UnityEngine;

[RequireComponent(typeof(RequesterTime))]
public class Clock : MonoBehaviour
{
    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private RotaterHands _rotater;

    private RequesterTime _requesterTime;
    private TimeSpan _currentTime = TimeSpan.Zero;
    private bool _isEditing;

    public event Action<TimeSpan> TimeChanged;

    public bool IsEditing => _isEditing;

    public TimeSpan CurrentTime => _currentTime;

    private void Awake()
    {
        _requesterTime = GetComponent<RequesterTime>();
    }

    private void Start()
    {
        _requesterTime.GetServerTime(OnServerTimeReceived);
    }

    private void Update()
    {
        if (_isEditing)
            return;

        AddSeconds(Time.deltaTime);
    }

    public void SetEditing(bool value)
    {
        _isEditing = value;
    }

    public void AddSeconds(double deltaSeconds)
    {
        SetTotalSeconds(_currentTime.TotalSeconds + deltaSeconds);
    }

    public void SetTime(int hour, int minute, int second)
    {
        hour = Mathf.Clamp(hour, ClockConstants.MIN_HOUR, ClockConstants.MAX_HOUR);
        minute = Mathf.Clamp(minute, ClockConstants.MIN_MINUTE, ClockConstants.MAX_MINUTE);
        second = Mathf.Clamp(second, ClockConstants.MIN_SECOND, ClockConstants.MAX_SECOND);

        _currentTime = new TimeSpan(hour, minute, second);
        NotifyTimeChanged();
    }

    public TimeSpan GetCurrentTime()
    {
        return _currentTime;
    }

    private void OnServerTimeReceived(DateTime serverTime)
    {
        SetTime(serverTime.Hour, serverTime.Minute, serverTime.Second);
    }

    private void SetTotalSeconds(double totalSeconds)
    {
        totalSeconds %= ClockConstants.SECONDS_PER_DAY;

        if (totalSeconds < 0)
            totalSeconds += ClockConstants.SECONDS_PER_DAY;

        _currentTime = TimeSpan.FromSeconds(totalSeconds);
        NotifyTimeChanged();
    }

    private void NotifyTimeChanged()
    {
        _rotater?.SetTime(_currentTime);
        _timePanel?.ShowTime(_currentTime.Hours, _currentTime.Minutes, _currentTime.Seconds);
        TimeChanged?.Invoke(_currentTime);
    }
}