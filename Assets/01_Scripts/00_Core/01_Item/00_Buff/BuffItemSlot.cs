using TMPro;
using UnityEngine;

public class BuffItemSlot : ItemSlot
{
    [SerializeField] private TextMeshProUGUI _tipText;
    private IItem _apply;
    public IItem Apply => _apply;

    private void Awake()
    {
        _tipText.gameObject.SetActive(false);
    }

    public void Set(ItemData item, IItem interfaceType)
    {
        base.Set(item);
        _apply = interfaceType;
        _tipText.gameObject.SetActive(true);
    }

    public override void Clear()
    {
        base.Clear();
        _apply = null;
        _tipText.gameObject.SetActive(false);
    }
}
