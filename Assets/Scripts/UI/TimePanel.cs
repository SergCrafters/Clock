using TMPro;
using UnityEngine;

public class TimePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hourText;
    [SerializeField] private TextMeshProUGUI _minuteText;
    [SerializeField] private TextMeshProUGUI _secondText;

    public void ShowTime(int hour, int minute, int second)
    {
        _hourText.text = hour.ToString(ClockConstants.TWO_DIGIT_FORMAT);
        _minuteText.text = minute.ToString(ClockConstants.TWO_DIGIT_FORMAT);
        _secondText.text = second.ToString(ClockConstants.TWO_DIGIT_FORMAT);
    }
}