using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;

    private Rigidbody _rigidbody;

    private void Start()
    {
        if (!TryGetComponent(out _rigidbody)) Logger.Log("rigid body is null");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & _groundLayerMask) != 0)
        {
            AutoJump();
        }
        else if (((1 << collision.gameObject.layer & _playerLayerMask) != 0))
        {

        }
    }

    private void AutoJump()
    {
        _rigidbody.AddForce(Vector3.up * Define.Item_Buff_JumpPower, ForceMode.Impulse);
    }
}
