using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// логика камеры
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] Vector3 offset;
    [SerializeField] float coefficient;

    float _currentAngleX = 0f;
    float _currentHeight = 0f;
    float _minHeight = 1f;
    float _maxHeight = 6f;

    void LateUpdate()
    {
        ChangeAngle();

        offset = new(5f * Mathf.Sin(_currentAngleX), _currentHeight, 5f * Mathf.Cos(_currentAngleX));

        camera.position = Vector3.Slerp(camera.position, target.localPosition + offset, Time.deltaTime * speed);
        camera.LookAt(target);
    }

    /// <summary>
    /// Получение текущей позиции тача/мыши
    /// </summary>
    /// <param name="context"></param>
    bool GetCurrentPosition(out Vector2 _delta)
    {
        _delta = Vector2.zero;
        if (Touchscreen.current == null) return false;

        foreach (TouchControl touch in Touchscreen.current.touches)
        {
            if (touch.isInProgress)
            {
                Vector2 _touchPosition = touch.position.ReadValue();
                if (_touchPosition.x < Screen.width / 2)
                    continue;

                _delta = touch.delta.ReadValue();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// смена угла
    /// </summary>
    void ChangeAngle()
    {
        if (!GetCurrentPosition(out Vector2 _input)) return;

        _currentAngleX += _input.x * coefficient;
        _currentHeight -= _input.y * coefficient;
        _currentHeight = Mathf.Clamp(_currentHeight, _minHeight, _maxHeight);
    }
}
/*
 * using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// логика камеры
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] Vector3 offset;
    [SerializeField] float coefficient;

    float _currentAngleX = 0f;
    float _currentHeight = 0f;
    float _minHeight = 1f;
    float _maxHeight = 6f;

    void LateUpdate()
    {
        ChangeAngle();

        offset = new(5f * Mathf.Sin(_currentAngleX), _currentHeight, 5f * Mathf.Cos(_currentAngleX));

        camera.position = Vector3.Slerp(camera.position, target.localPosition + offset, Time.deltaTime * speed);
        camera.LookAt(target);
    }

    /// <summary>
    /// Получение текущей позиции тача/мыши
    /// </summary>
    /// <param name="context"></param>
    bool GetCurrentPosition(out Vector2 _touchPosition)
    {
        _touchPosition = Vector2.zero;
        if (Touchscreen.current == null) return false;

        foreach (TouchControl touch in Touchscreen.current.touches)
        {
            if (touch.isInProgress)
            {
                _touchPosition = touch.position.ReadValue();
                if (_touchPosition.x < Screen.width / 2) 
                    continue;

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// смена угла
    /// </summary>
    void ChangeAngle()
    {
        if (!GetCurrentPosition(out Vector2 _input)) return;

        _currentAngleX += _input.x * coefficient;
        _currentHeight -= _input.y * coefficient;
        _currentHeight = Mathf.Clamp(_currentHeight, _minHeight, _maxHeight);
    }
}
*/