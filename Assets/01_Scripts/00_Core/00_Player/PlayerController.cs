using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    WallSlide,
    Climb
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashMoveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _wallLayerMask;
    private Vector2 _curMovementInput;

    // player state
    private PlayerState _curState = PlayerState.Idle;

    // Dash
    private bool _isDash = false;
    public Action OnDash;

    // Climb
    private bool _isClimb = false;
    public Action OnClimb;

    // Stmina
    public bool CanSpendStamina { get; set; }

    // Jump
    private bool _canJump = true;
    private bool _canDoubleJump = true;

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
    private PlayerAnimation _animHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animHandler = GetComponentInChildren<PlayerAnimation>();
    }

    private void FixedUpdate()
    {
        switch (_curState)
        {
            case PlayerState.Move:
            case PlayerState.Jump:
                Move();
                break;
            case PlayerState.WallSlide:
                WallSlide();
                break;
        }
    }

    private void LateUpdate()
    {
        Look();
    }

    #region 플레이어 상태 관리
    public void ChangePlayerState(PlayerState state)
    {
        Logger.Log($"상태 변경 {_curState} -> {state}");
        _curState = state;
    }

    public void BuffJumpPower(float value)
    {
        _jumpPower += value;
    }

    public void ResetJumpPower(float value)
    {
        _jumpPower -= value;
    }

    public void BuffSpped(float value)
    {
        _moveSpeed *= value;
        _dashMoveSpeed *= value;
    }

    public void ResetSpeed(float value)
    {
        _moveSpeed /= value;
        _dashMoveSpeed /= value;
    }
    #endregion

    #region 플레이어 움직임(Movement)
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _curMovementInput = context.ReadValue<Vector2>();
            ChangePlayerState(PlayerState.Move);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _curMovementInput = Vector2.zero;
            ChangePlayerState(PlayerState.Idle);
            _animHandler.Idle();
        }
    }

    public void OnJumpInpupt(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsGrounded();
            if (_canJump)
            {
                Logger.Log("점프");
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);
                _canJump = false;
                ChangePlayerState(PlayerState.Jump);
            }
            else if (_canDoubleJump)
            {
                Logger.Log("더블 점프");
                _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode.Impulse);
                _canDoubleJump = false;
                ChangePlayerState(PlayerState.Jump);
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
        if (context.phase == InputActionPhase.Performed && IsWall())
        {
            Logger.Log("등반 시작");
            _isClimb = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            Logger.Log("등반 종료");
            _isClimb = false;
        }
    }

    public void SuperJump(float superJumpPower)
    {
        Logger.Log("슈퍼 점프");
        _rigidbody.AddForce(Vector2.up * superJumpPower, ForceMode.Impulse);
    }

    private void Move()
    {
        if (_isDash) OnDash?.Invoke();
        if (_isClimb) OnClimb?.Invoke();

        Vector3 direction =
            (_isClimb ? transform.up : transform.forward) * _curMovementInput.y +
            transform.right * _curMovementInput.x;

        float speed;
        if (CanSpendStamina)
        {
            if (_isDash)
            {
                speed = Define.Player_DashSpeed;
                _animHandler.Dash();
            }
            else if (_isClimb)
            {
                speed = Define.Player_ClimbSpeed;
            }
            else
            {
                speed = Define.Player_MoveSpeed;
                _animHandler.Walk();
            }
        }
        else
        {
            if (_isClimb)
            {
                ChangePlayerState(PlayerState.WallSlide);
                return;
            }
            speed = Define.Player_MoveSpeed;
            _animHandler.Walk();
        }

        //Logger.Log($"속도: {speed}");
        direction *= speed;

        if (!_isClimb)
        {
            direction.y = _rigidbody.velocity.y;
        }

        if (!_isClimb && IsWall() && !IsGrounded())
        {
            ChangePlayerState(PlayerState.WallSlide);
            return;
        }

        _rigidbody.velocity = direction;
    }

    private void WallSlide()
    {
        if (!IsWall())
        {
            Logger.Log("벽에서 떨어짐");
            ChangePlayerState(PlayerState.Jump);
            return;
        }

        if (IsGrounded())
        {
            Logger.Log("땅에 착지");
            ChangePlayerState(PlayerState.Move);
            return;
        }
    }

    private bool IsGrounded()
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
                return true;
            }
        }

        _canJump = false;
        return false;
    }

    private bool IsWall()
    {
        Ray ray = new Ray(transform.position + (transform.up * 1.5f), transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.red, 1f);
        return Physics.Raycast(ray, 0.5f, _wallLayerMask);
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

    public void OnUseItemInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Logger.Log("버프 아이템 사용");
            Managers.Instance.Game.Player?.UseBuffItem();
        }
    }
    #endregion
}
