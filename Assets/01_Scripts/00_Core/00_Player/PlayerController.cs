using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashMoveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundLayerMask;
    private Vector2 _curMovementInput;

    // Dash
    private bool _isDash = false;
    public bool IsDash => _isDash;
    public Action OnDash;

    // Stmina
    public bool CanSpendStamina { get; set; }

    // Jump
    private bool _canJump = true;
    private bool _canDoubleJump = true;

    // Climb
    private bool _isClimb = false;

    [Header("Look")]
    [SerializeField] private Transform _cameraContainer;
    [SerializeField] private Camera _firstPersonCamera;
    [SerializeField] private Camera _thirdPersonCamera;
    [SerializeField] private float _minXLook;
    [SerializeField] private float _maxXLook;
    [SerializeField] private float _lookSensitivity;
    private float _curCameraXRotation;
    private Vector2 _mouseDelta;

    // component cache
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsDash) OnDash?.Invoke();
    }

    private void FixedUpdate()
    {
        if (_isClimb)
        {

        }
        else
        {
            Move();
        }
    }

    private void LateUpdate()
    {
        Look();
    }

    #region 플레이어 움직임(Movement)
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInpupt(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            CheckGround();
            if (_canJump)
            {
                Logger.Log("점프");
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);
                _canJump = false;
            }
            else if (_canDoubleJump)
            {
                Logger.Log("더블 점프");
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);
                _canDoubleJump = false;
            }
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Logger.Log("대시 시작");
            _isDash = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Logger.Log("대시 취소");
            _isDash = false;
        }
    }

    public void OnClimbInput(InputAction.CallbackContext context)
    {
        // 나중에 구현
    }

    public void SuperJump(float superJumpPower)
    {
        Logger.Log("슈퍼 점프");
        _rigidbody.AddForce(Vector2.up * superJumpPower, ForceMode.Impulse);
    }

    private void Move()
    {
        Vector3 direction =
            transform.forward * _curMovementInput.y +
            transform.right * _curMovementInput.x;
        float speed = (_isDash && CanSpendStamina ? _dashMoveSpeed : _moveSpeed);
        Logger.Log($"speed: {speed}");
        direction *= speed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
    }

    private void CheckGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        foreach (var ray in rays)
        {
            if (Physics.Raycast(ray, 0.1f, _groundLayerMask))
            {
                ResetJumpFlags();
                return;
            }
        }

        _canJump = false;
    }

    private void ResetJumpFlags()
    {
        _canJump = true;
        _canDoubleJump = true;
    }
    #endregion

    #region 플레이어 화면(Look)
    public void OnLookInput(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnChangeLookInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (_firstPersonCamera.gameObject.activeSelf)
            {
                Logger.Log("3인칭 시점으로 변경");
                _firstPersonCamera.gameObject.SetActive(false);
                _thirdPersonCamera.gameObject.SetActive(true);
            }
            else
            {
                Logger.Log("1인칭 시점으로 변경");
                _firstPersonCamera.gameObject.SetActive(true);
                _thirdPersonCamera.gameObject.SetActive(false);
            }
        }
    }

    public void Look()
    {
        _curCameraXRotation += _mouseDelta.y * _lookSensitivity;
        _curCameraXRotation = Mathf.Clamp(_curCameraXRotation, _minXLook, _maxXLook);
        _cameraContainer.localEulerAngles = new Vector3(-_curCameraXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, _mouseDelta.x * _lookSensitivity, 0);
    }
    #endregion

    #region 인벤토리
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("인벤토리 열기");
            UIManager.Instance.Inventory.ToggleInventory();
        }
    }
    #endregion
}
