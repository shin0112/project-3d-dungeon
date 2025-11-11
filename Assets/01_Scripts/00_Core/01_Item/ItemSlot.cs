using UnityEngine;
using UnityEngine.UI;

public class ItemSlot<T> : MonoBehaviour
{
    [Header("Item data")]
    [SerializeField] private ItemData _item;
    [SerializeField] private int _index;
    private T _apply;

    public ItemData Item => _item;
    public T Apply => _apply;

    [Header("UI")]
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.gameObject.SetActive(false);
    }

    public void Set(ItemData item, T interfaceType)
    {
        _item = item;
        if (item.Type == ItemType.Equipment)
        {
            _icon.sprite = item.Icon;
        }
        _apply = interfaceType;
    }

    public void Clear()
    {
        _item = null;
        _icon.sprite = null;
    }

    public bool IsEmpty()
    {
        return Item == null;
    }
}
