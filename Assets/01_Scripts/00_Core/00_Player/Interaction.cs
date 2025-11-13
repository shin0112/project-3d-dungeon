using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("Check Settings")]
    [SerializeField] private float _checkRate = 0.05f;
    [SerializeField] private float _maxCheckDistance;
    [SerializeField] private LayerMask _generalLayerMask;
    [SerializeField] private LayerMask _buffItemLayerMask;
    private float _lastCheckTime;

    [Header("Object Interacting")]
    [SerializeField] private GameObject _curInteractGameObject;
    private IInteractable _curInteratable;

    // ui
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - _lastCheckTime > _checkRate)
        {
            _lastCheckTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxCheckDistance, _generalLayerMask))
            {
                if (hit.collider.gameObject != _curInteractGameObject)
                {
                    _curInteractGameObject = hit.collider.gameObject;
                    _curInteratable = hit.collider.GetComponent<IInteractable>();
                    Managers.Instance.UI.Prompt.Mode = PromptMode.General;
                    SetPromptUI();
                }
            }
            else if (Physics.Raycast(ray, out hit, _maxCheckDistance, _buffItemLayerMask))
            {
                if (hit.collider.gameObject != _curInteractGameObject)
                {
                    _curInteractGameObject = hit.collider.gameObject;
                    _curInteratable = hit.collider.GetComponent<IInteractable>();
                    Managers.Instance.UI.Prompt.Mode = PromptMode.Buff;
                    SetPromptUI();
                }
            }
            else
            {
                _curInteractGameObject = null;
                _curInteratable = null;
                UIManager.Instance.OnEndInteraction?.Invoke();
            }
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && _curInteratable != null)
        {
            _curInteratable.OnInteract();
            _curInteratable = null;
            _curInteractGameObject = null;
            UIManager.Instance.OnEndInteraction?.Invoke();
        }
    }

    private void SetPromptUI()
    {
        // 0: name, 1: description
        string[] itemData = _curInteratable.GetInteractPrompt();
        UIManager.Instance.OnPromptChanged?.Invoke(itemData[0], itemData[1]);
    }
}
