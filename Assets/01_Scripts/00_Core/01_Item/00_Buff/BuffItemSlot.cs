using TMPro;
using UnityEngine;

public class BuffItemSlot : ItemSlot
{
    [SerializeField] private TextMeshProUGUI _tipText;

    private void Awake()
    {
        _tipText.gameObject.SetActive(false);
    }

    public override void Set(ItemData item)
    {
        base.Set(item);
        _tipText.gameObject.SetActive(true);
    }

    public override void Clear()
    {
        base.Clear();
        _tipText.gameObject.SetActive(false);
    }
}
