using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
}

public enum ConsumableType
{
    None,
    Health,
    Buff
}

public enum EquipmentType
{
    None,
    Hand,
    Hat,
    Top,
    Shoes
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType Type { get; }
    public float Value { get; }
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] private Color _color;
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public ItemType Type { get { return _type; } }
    public Color Color { get { return _color; } }

    [Header("Consumable")]
    [SerializeField] private ConsumableType _consumableType;
    [SerializeField] private ItemDataConsumable[] _consumables;
    public ConsumableType ConsumableType { get { return _consumableType; } }

    [Header("Equipment")]
    [SerializeField] private EquipmentType _equipmentType;
    public EquipmentType EquipmentType { get { return _equipmentType; } }
}
