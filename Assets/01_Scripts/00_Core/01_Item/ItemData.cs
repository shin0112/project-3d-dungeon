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

public enum BuffType
{
    InfiniteStamina,
    SpeedBoost,
    HighJump,
}

public enum EquipmentType
{
    None,
    Hand,
    Hat,
    Top,
    Shoes
}

public enum StatType
{
    MaxHealth,
    MaxStamina,
    JumpPower,
}

[System.Serializable]
public class ItemDataConsumable
{
    [SerializeField] private ConsumableType _type;
    [SerializeField] private float _value;
    public ConsumableType Type => _type;
    public float Value => _value;
}

[System.Serializable]
public class ItemDataBuff
{
    [SerializeField] private BuffType _type;
    [SerializeField] private float _value;
    public BuffType Type => _type;
    public float Value => _value;
}

[System.Serializable]
public class ItemDataEquipment
{
    [SerializeField] private StatType _type;
    [SerializeField] private float _value;
    public StatType Type => _type;
    public float Value => _value;
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
    public string Name => _name;
    public string Description => _description;
    public ItemType Type => _type;
    public Sprite Icon => _icon;
    public GameObject DropPrefab => _dropPrefab;
    public Color Color => _color;

    [Header("Consumable")]
    [SerializeField] private ConsumableType _consumableType;
    [SerializeField] private ItemDataConsumable[] _consumables;
    public ConsumableType ConsumableType { get { return _consumableType; } }

    [Header("Buff")]
    [SerializeField] private ItemDataBuff _buff;
    public ItemDataBuff Buff => _buff;

    [Header("Equipment")]
    [SerializeField] private EquipmentType _equipmentType;
    [SerializeField] private ItemDataEquipment[] _equipments;
    public EquipmentType EquipmentType { get { return _equipmentType; } }
    public ItemDataEquipment[] ItemDataEquipments => _equipments;
}
