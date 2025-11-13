using TMPro;
using UnityEngine;

public class BuffItemSlot : ItemSlot
{
    [SerializeField] private TextMeshProUGUI _tipText;

    private void Awake()
    {
        _tipText.gameObject.SetActive(false);
    }

    public override void Set(ItemData item, IItem interfaceType)
    {
        base.Set(item, interfaceType);
        _tipText.gameObject.SetActive(true);
    }

    public override void Clear()
    {
        base.Clear();
        _tipText.gameObject.SetActive(false);
    }
}
