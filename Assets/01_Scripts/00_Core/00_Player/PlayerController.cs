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

    #endregion
}
