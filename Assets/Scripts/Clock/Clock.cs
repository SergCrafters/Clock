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
    private TimeSpan _editStartTime;
    private double _editStartRealtime;

    public event Action<TimeSpan> TimeChanged;

    public bool IsEditing => _isEditing;
    public TimeSpan CurrentTime => _currentTime;

    private void Awake()
    {
        _requesterTime = GetComponent<RequesterTime>();
    }

    private void Start()
    {
        RequestServerTime();
    }

    private void Update()
    {
        if (_isEditing)
            return;

        AddSeconds(Time.deltaTime);
    }

    public void BeginEditing()
    {
        if (_isEditing)
            return;

        _isEditing = true;
        _editStartTime = _currentTime;
        _editStartRealtime = Time.realtimeSinceStartupAsDouble;
    }

    public void SaveEditing()
    {
        _isEditing = false;
    }

    public void CancelEditing()
    {
        if (!_isEditing)
            return;

        double elapsedSeconds = Time.realtimeSinceStartupAsDouble - _editStartRealtime;
        SetTotalSeconds(_editStartTime.TotalSeconds + elapsedSeconds);

        _isEditing = false;
    }

    public void RequestServerTime(Action onCompleted = null)
    {
        _requesterTime.GetServerTime(serverTime =>
        {
            SetTime(serverTime.Hour, serverTime.Minute, serverTime.Second);
            onCompleted?.Invoke();
        });
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