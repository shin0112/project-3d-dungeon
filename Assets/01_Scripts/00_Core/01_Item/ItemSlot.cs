using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("Item data")]
    [SerializeField] private ItemData _item;
    [SerializeField] private int _index;

    [Header("UI")]
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void SetItemSlot(ItemData item)
    {
        _item = item;
        if (item.Type == ItemType.Equipment)
        {
            _icon.sprite = item.Icon;
        }
    }
}
