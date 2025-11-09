using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private LayerMask _groundLayerMask;
    private Vector2 _currentMovementInput;

    private bool _canJump;
    private bool _canDoubleJump;

    // component
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        ResetJumpFlags();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region 플레이어 행동 로직
    public void Move()
    {
        Vector3 direction =
            transform.forward * _currentMovementInput.y +
            transform.right * _currentMovementInput.x;
        direction *= _moveSpeed;
        direction.y = _rigidbody.velocity.y;

        _rigidbody.velocity = direction;
    }
    #endregion

    #region 플레이어 행동 입력
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _currentMovementInput = Vector2.zero;
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
    #endregion

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
                _canJump = true;
                _canDoubleJump = true;
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
}
