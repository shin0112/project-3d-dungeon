using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
}

public enum ConsumableType
{
    Health,
    Buff
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
    public Color Color { get { return _color; } }

    [Header("Consumable")]
    [SerializeField] ItemDataConsumable[] _consumables;
}
