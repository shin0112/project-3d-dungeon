using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Rigidbody _rigidbody;

    private void Start()
    {
        if (!TryGetComponent(out _rigidbody)) Logger.Log("rigid body is null");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & _layerMask) != 0)
        {
            AutoJump();
        }
    }

    private void AutoJump()
    {
        _rigidbody.AddForce(Vector3.up * Define.BuffItemJumpPower, ForceMode.Impulse);
    }
}
