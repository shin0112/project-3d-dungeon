using TMPro;
using UnityEngine;

public class BuffItemSlot : ItemSlot
{
    [SerializeField] private TextMeshProUGUI _tipText;
    private IConsumable _apply;
    public IConsumable Apply => _apply;

    private void Awake()
    {
        _tipText.gameObject.SetActive(false);
    }

    public void Set(ItemData item, IConsumable interfaceType)
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
