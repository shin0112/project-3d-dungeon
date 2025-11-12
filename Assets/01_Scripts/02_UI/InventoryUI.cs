using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region 변수
    [Header("Inventory Settings")]
    [SerializeField] private float _slideDuration = 1f;
    [SerializeField] private float _slideDistance = 700f;
    private RectTransform _rectTransform;
    private bool _isClosed = true;

    [Header("Arrow Button")]
    [SerializeField] private Button _arrowButton;
    [SerializeField] private Image _arrowButtonImg;

    [Header("Item Data")]
    private ItemSlot<IEquipment>[] _slots;

    [Header("Buff Item Info")]
    private ItemSlot<IConsumable> _buffitem;

    [Header("Selected Item Info")]
    private ItemSlot<IEquipment> _selectedItem;
    private int _selectedItemIndex;
    [SerializeField] private TextMeshProUGUI _seleectedItemName;
    [SerializeField] private TextMeshProUGUI _seleectedItemDescription;

    private PlayerController _controller;
    private PlayerCondition _condition;
    #endregion

    private void Awake()
    {
        if (TryGetComponent(out _rectTransform)) Logger.Log("rect transform is null");
    }

    private void OnEnable()
    {
        Logger.Log("버튼 등록");
        _arrowButton.onClick.AddListener(ToggleInventory);
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;

        _condition = player.PlayerCondition;
        _controller = player.PlayerController;

        player.UseBuffItem += UserBuffItem;
    }

    private void OnDisable()
    {
        _arrowButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        GameManager.Instance.Player.UseBuffItem -= UserBuffItem;
    }

    #region 인벤토리 창 세팅
    public void ToggleInventory()
    {
        Logger.Log("인벤토리 여닫기");
        StopCoroutine(MoveInventoryUI());
        StartCoroutine(MoveInventoryUI());
    }

    private IEnumerator MoveInventoryUI()
    {
        Vector2 start = _rectTransform.anchoredPosition;
        Vector2 end = start + new Vector2(0, _isClosed ? -_slideDistance : _slideDistance);

        float elapsed = 0f;
        while (elapsed < _slideDuration)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(start, end, elapsed / _slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = end;
        FlipArrow();

        Logger.Log("인벤토리 여닫기 코루틴 종료");
    }

    private void FlipArrow()
    {
        if (_isClosed)
        {
            _arrowButtonImg.rectTransform.rotation = Quaternion.Euler(0, 0, 180f);
            _arrowButtonImg.rectTransform.position += Vector3.up * 100f;
        }
        else
        {
            _arrowButtonImg.rectTransform.rotation = Quaternion.Euler(0, 0, 0f);
            _arrowButtonImg.rectTransform.position += Vector3.down * 100f;
        }

        _isClosed = !_isClosed;
    }
    #endregion

    #region 인벤토리 슬롯 세팅
    /// <summary>
    /// 버프 아이템 슬롯 채우기
    /// </summary>
    public void AddBuffItem(ItemData item, IConsumable interfaceType)
    {
        _buffitem.Set(item, interfaceType);
    }

    /// <summary>
    /// 버프 아이템 사용하기
    /// </summary>
    private void UserBuffItem()
    {
        if (_buffitem.IsEmpty()) return;
        _buffitem.Apply.ApplyEffect();
    }
    #endregion
}
