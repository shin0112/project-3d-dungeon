using UnityEngine;

public class BuffItemController : MonoBehaviour
{
    [Header("Layer Mask")]
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;

    [Header("Item")]
    private ItemData _data;
    private IConsumable _consumable;

    private Rigidbody _rigidbody;

    private void Start()
    {
        if (TryGetComponent(out ItemObject itemObject))
        {
            _data = itemObject.Data;
        }
        else
        {
            Logger.Log("Item Object is null");
        }
        if (!TryGetComponent(out _consumable)) Logger.Log("consumable is null");
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
            UIManager.Instance.Inventory.AddBuffItem(_data, _consumable);
        }
    }

    private void AutoJump()
    {
        _rigidbody.AddForce(Vector3.up * Define.Item_Buff_JumpPower, ForceMode.Impulse);
    }
}
