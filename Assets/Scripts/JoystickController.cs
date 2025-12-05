using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform joystick;

    public static UnityAction<Vector2> SetDirection;

    Vector2 _startPosition;
    const float _maxDirañtion = 200f;
    Coroutine _returnCoroutine = null;

    void Start()
    {
        _startPosition = joystick.position; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_returnCoroutine != null)
            StopCoroutine(ReturnToStartPosition());

        Vector2 _direction = eventData.position - _startPosition;
        float _lenght = Mathf.Min(_direction.magnitude, _maxDirañtion);

        joystick.position = _startPosition + _lenght * _direction.normalized;
        SetDirection?.Invoke(_direction);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetDirection?.Invoke(Vector2.zero);

        _returnCoroutine = StartCoroutine(ReturnToStartPosition());
    }

    IEnumerator ReturnToStartPosition()
    {
        float _duration = 0.1f;
        float _elapsed = 0f;
        Vector2 _currentPosition = joystick.position;

        while (_elapsed < _duration)
        {
            _elapsed += Time.deltaTime;
            joystick.position = Vector2.Lerp(_currentPosition, _startPosition, _elapsed / _duration);

            yield return null;
        }
    }

}
