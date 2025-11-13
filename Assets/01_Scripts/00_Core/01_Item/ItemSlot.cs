using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("Item data")]
    [SerializeField] private ItemData _item;
    [SerializeField] private int _index;

    public ItemData Item => _item;

    [Header("UI")]
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _defaultSprite;
    private Button _button;
    private Outline _outline;

    private void Awake()
    {
        if (!TryGetComponent(out _button)) { Logger.Warning("button is null"); }
        if (!TryGetComponent(out _outline)) { Logger.Warning("outline is null"); }
        if (_defaultSprite == null) _defaultSprite = _icon.sprite;
    }

    public virtual void Set(ItemData item)
    {
        _icon.gameObject.SetActive(true);
        _item = item;
        _icon.sprite = item.Icon;

        Logger.Log($"아이템 슬롯 세팅 완료: {_item.Name}");
    }

    public virtual void Clear()
    {
        _item = null;
        _icon.sprite = _defaultSprite;

        Logger.Log("아이템 슬롯 클리어 완료");
    }

    public bool IsEmpty()
    {
        return Item == null;
    }
}
