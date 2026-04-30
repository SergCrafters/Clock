using UnityEngine;
using UnityEngine.UI;

public class EditButton : MonoBehaviour
{
    [SerializeField] private EditTimePanel _editPanel;
    [SerializeField] private Button _editButton;

    private void OnEnable()
    {
        _editButton.onClick.AddListener(_editPanel.Open);
    }

    private void OnDisable()
    {
        _editButton.onClick.RemoveListener(_editPanel.Open);
    }
}