using UnityEngine;

public class JumpPad : MonoBehaviour, IImpactable
{
    [SerializeField] private float _jumpPower;

    private PlayerController _controller;
    private LayerMask _collisionLayer;
    private bool _canJump = true;

    public void Init(PlayerController controller, LayerMask layerMask)
    {
        _controller = controller;
        _collisionLayer = layerMask;
    }

    public void ModifyMovement()
    {
        _controller.SuperJump(_jumpPower);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Logger.Log("충돌 감지");

        if (((1 << collision.gameObject.layer) & _collisionLayer) != 0 && _canJump)
        {
            ModifyMovement();
            _canJump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Logger.Log("충돌 종료");

        _canJump = true;
    }
}
