using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private ItemObject _itemObject;
    private Rigidbody _rigidbody;

    private void Start()
    {
        if (!TryGetComponent(out _itemObject)) Logger.Log("item object is null");
        if (!TryGetComponent(out _rigidbody)) Logger.Log("rigid body is null");
    }

    private void Update()
    {
        AutoJump();
    }

    private void AutoJump()
    {
        Ray ray = new Ray(transform.position + (transform.up * 0.01f), Vector3.down);

        if (Physics.Raycast(ray, Define.BuffItemSize, _layerMask))
        {
            Logger.Log("auto jump 도는 중");

            _rigidbody.AddForce(Vector3.up * Define.BuffItemJumpPower, ForceMode.Impulse);
        }
    }
}
