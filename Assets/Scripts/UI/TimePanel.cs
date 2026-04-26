using TMPro;
using UnityEngine;

public class TimePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hourText;
    [SerializeField] private TextMeshProUGUI _minuteText;
    [SerializeField] private TextMeshProUGUI _secondText;

    public void ShowTime(float hour, float minute, float second)
    {
        _hourText.text = hour.ToString("00");
        _minuteText.text = minute.ToString("00");
        _secondText.text = second.ToString("00");
    }
}
