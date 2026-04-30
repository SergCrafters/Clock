using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditTimePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _hourInput;
    [SerializeField] private TMP_InputField _minuteInput;
    [SerializeField] private TMP_InputField _secondInput;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _serverTimeButton;

    [SerializeField] private Clock _clock;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(Save);
        _cancelButton.onClick.AddListener(Cancel);
        _serverTimeButton.onClick.AddListener(ReturnServerTime);

        _clock.TimeChanged += OnClockTimeChanged;
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(Save);
        _cancelButton.onClick.RemoveListener(Cancel);
        _serverTimeButton.onClick.RemoveListener(ReturnServerTime);

        _clock.TimeChanged -= OnClockTimeChanged;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _clock.BeginEditing();
        SetInputs(_clock.GetCurrentTime());
    }

    private void Save()
    {
        int hours = ReadClampedValue(_hourInput.text, ClockConstants.MIN_HOUR, ClockConstants.MAX_HOUR);
        int minutes = ReadClampedValue(_minuteInput.text, ClockConstants.MIN_MINUTE, ClockConstants.MAX_MINUTE);
        int seconds = ReadClampedValue(_secondInput.text, ClockConstants.MIN_SECOND, ClockConstants.MAX_SECOND);

        _clock.SetTime(hours, minutes, seconds);
        _clock.SaveEditing();
        Close();
    }

    private void Cancel()
    {
        _clock.CancelEditing();
        Close();
    }

    private void ReturnServerTime()
    {
        _clock.RequestServerTime(() =>
        {
            _clock.SaveEditing();
            Close();
        });
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnClockTimeChanged(TimeSpan time)
    {
        if (!gameObject.activeInHierarchy)
            return;

        SetInputs(time);
    }

    private void SetInputs(TimeSpan time)
    {
        _hourInput.text = time.Hours.ToString(ClockConstants.TWO_DIGIT_FORMAT);
        _minuteInput.text = time.Minutes.ToString(ClockConstants.TWO_DIGIT_FORMAT);
        _secondInput.text = time.Seconds.ToString(ClockConstants.TWO_DIGIT_FORMAT);
    }

    private int ReadClampedValue(string value, int min, int max)
    {
        if (!int.TryParse(value, out int result))
            result = min;

        return Mathf.Clamp(result, min, max);
    }
}