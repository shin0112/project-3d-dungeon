using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private float _slideDuration = 1f;
    [SerializeField] private float _slideDistance = 700f;
    private RectTransform _rectTransform;
    private bool _isClosed = true;

    [Header("Arrow Button")]
    [SerializeField] private Button _arrowButton;
    [SerializeField] private Image _arrowButtonImg;

    private void Awake()
    {
        if (TryGetComponent(out _rectTransform)) Logger.Log("rect transform is null");
    }

    private void OnEnable()
    {
        Logger.Log("버튼 등록");
        _arrowButton.onClick.AddListener(ToggleInventory);
    }

    private void OnDisable()
    {
        _arrowButton.onClick.RemoveAllListeners();
    }

    private void ToggleInventory()
    {
        Logger.Log("버튼 클릭");
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
}
