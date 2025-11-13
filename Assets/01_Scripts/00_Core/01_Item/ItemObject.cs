using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData _data;
    private Renderer _renderer;

    public ItemData Data => _data;

    private void Awake()
    {
        if (_data.Type == ItemType.Consumable)
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material.color = _data.Color;
        }
    }

    public string[] GetInteractPrompt()
    {
        return new string[] { _data.Name, _data.Description };
    }

    public void OnInteract()
    {
        if (_data.Type == ItemType.Consumable && _data.ConsumableType == ConsumableType.Buff) return;

        if (_data.Type == ItemType.Equipment)
        {
            UIManager.Instance.Inventory.AddEquipmentItem(_data);
            Managers.Instance.Game.Player.EquipHat(_data);
        }
    }
}
