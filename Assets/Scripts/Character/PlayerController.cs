using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerTriggerController trigger;
    [SerializeField] InventoryController inventory;
    [SerializeField] Transform player;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    Vector2 _direction;

    public Transform Player => player;
    public CharacterController Controller => characterController;

    InputSystem_Actions _inputActions;

    Transform _cameraTransform;

    float _gravitationForce = 1000f;


    void Awake()
    {
        _inputActions = new();
    }

    void OnEnable()
    {
        _inputActions.Player.Attack.Enable();
        _inputActions.Player.Attack.performed += Mine;

        JoystickController.SetDirection += SetDirection;
    }

    void OnDisable()
    {
        _inputActions.Player.Attack.Disable();
        _inputActions.Player.Attack.performed -= Mine;

        JoystickController.SetDirection -= SetDirection;
    }

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        StartCoroutine(Moving());
    }

    void SetDirection(Vector2 _direction)
    {
        this._direction = _direction;
    }

    IEnumerator Moving()
    {
        while (true)
        {
            Vector3 _resultDirection = GetDirection();
            characterController.Move(_resultDirection * Time.deltaTime);

            if (_direction != Vector2.zero)
            {
                Quaternion _targetRotation = Quaternion.LookRotation(new Vector3(_resultDirection.x, 0, _resultDirection.z));
                player.rotation = Quaternion.Slerp(player.rotation, _targetRotation, Time.deltaTime * rotationSpeed);
            }

            trigger.transform.SetPositionAndRotation(transform.position, transform.rotation);

            yield return null;
        }
    }

    Vector3 GetDirection()
    {
        Vector3 _cameraForward = _cameraTransform.forward;
        Vector3 _cameraRight = _cameraTransform.right;

        _cameraForward.y = 0;
        _cameraRight.y = 0;

        _cameraForward.Normalize();
        _cameraRight.Normalize();

        return (_cameraForward * _direction.y + Vector3.down * _gravitationForce + _cameraRight * _direction.x) * speed;
    }

    void Mine(InputAction.CallbackContext context)
    {
        foreach (Resource _resource in trigger.Resources)
        {
            Debug.Log(_resource);
            inventory.AddItem(_resource.Type);
            Destroy(_resource.gameObject);
        }
    }

    
}
