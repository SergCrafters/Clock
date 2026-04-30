using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class ClockHandDrag : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Clock _clock;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _secondsPerFullTurn = ClockConstants.SECONDS_PER_MINUTE;

    private bool _dragging;
    private float _lastPointerAngle;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_clock == null || !_clock.IsEditing)
            return;

        _dragging = true;
        _lastPointerAngle = GetPointerAngle(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_dragging || _clock == null || !_clock.IsEditing)
            return;

        float currentAngle = GetPointerAngle(eventData);
        float deltaAngle = Mathf.DeltaAngle(_lastPointerAngle, currentAngle);
        _lastPointerAngle = currentAngle;

        float deltaSeconds = -deltaAngle * (_secondsPerFullTurn / ClockConstants.FULL_CIRCLE_DEGREES);
        _clock.AddSeconds(deltaSeconds);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
    }

    private float GetPointerAngle(PointerEventData eventData)
    {
        Camera cam = eventData.pressEventCamera != null ? eventData.pressEventCamera : _camera;
        if (cam == null)
            return 0f;

        Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);
        Vector3 worldPosition = cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, screenPosition.z));

        Vector2 direction = worldPosition - transform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}