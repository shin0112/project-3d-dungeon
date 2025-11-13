using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("Item data")]
    [SerializeField] private ItemData _item;
    [SerializeField] private int _index;
    private IItem _apply;

    public ItemData Item => _item;
    public IItem Apply => _apply;

    [Header("UI")]
    [SerializeField] private Image _icon;
    private Button _button;
    private Outline _outline;

    private void Awake()
    {
        if (!TryGetComponent(out _button)) { Logger.Warning("button is null"); }
        if (!TryGetComponent(out _outline)) { Logger.Warning("outline is null"); }
    }

    public virtual void Set(ItemData item, IItem interfaceType)
    {
        _icon.gameObject.SetActive(true);
        _item = item;
        _icon.sprite = item.Icon;
        _apply = interfaceType;

        Logger.Log($"아이템 슬롯 세팅 완료: {_item.Name}");
    }

    public virtual void Clear()
    {
        _item = null;
        _icon.sprite = null;
        _apply = null;

        Logger.Log("아이템 슬롯 클리어 완료");
    }

    public bool IsEmpty()
    {
        return Item == null;
    }
}
