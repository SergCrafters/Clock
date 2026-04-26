using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditTimePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _hourInput;
    [SerializeField] private TMP_InputField _minuteInput;
    [SerializeField] private TMP_InputField _secondInput;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Clock _clock;

    private void OnEnable()
    {
        _saveButton.onClick.AddListener(Edit);
        _saveButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _saveButton.onClick.RemoveListener(Edit);
        _saveButton.onClick.RemoveListener(Close);
    }

    public void Open()
    {
        SetCurrentTimeToInputs();
        gameObject.SetActive(true);
    }

    private void Edit()
    {
        int hours = int.Parse(_hourInput.text);
        if (CheckOnNegative(hours))
            hours = 0;

        int minutes = int.Parse(_minuteInput.text);
        if (CheckOnNegative(minutes))
            minutes = 0;

        int seconds = int.Parse(_secondInput.text);
        if (CheckOnNegative(seconds))
            seconds = 0;

        _clock.EditTime(hours, minutes, seconds);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private bool CheckOnNegative(int value)
    {
        if (value < 0)
            return true;

        return false;
    }

    private void SetCurrentTimeToInputs()
    {
        if (_clock != null)
        {
            var currentTime = _clock.GetCurrentTime();
            _hourInput.text = currentTime.Hours.ToString("D2");
            _minuteInput.text = currentTime.Minutes.ToString("D2");
            _secondInput.text = currentTime.Seconds.ToString("D2");
        }
    }
}
