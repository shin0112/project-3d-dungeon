using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("Player UI")]
    [SerializeField] private PlayerConditionUI _playerCondition;
    [SerializeField] private PromptUI _prompt;
    [SerializeField] private InventoryUI _inventory;
    public InventoryUI Inventory => _inventory;

    private bool _isHall = true;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        GetComponents();

        AddPlayerConditionActions();
        AddPromptActions();
    }

    private void GetComponents()
    {
        if (_playerCondition == null)
        {
            Logger.Log("player condition ui is null");
            _playerCondition = GetComponentInChildren<PlayerConditionUI>();
        }

        if (_prompt == null)
        {
            Logger.Log("prompt ui is null");
            _prompt = GetComponentInChildren<PromptUI>();
        }

        if (_inventory == null)
        {
            Logger.Log("inventory ui is null");
            _inventory = GetComponentInChildren<InventoryUI>();
        }
    }

    private void OnDestroy()
    {
        RemovePlayerConditionActions();
        RemovePromptActions();
    }

    #region 플레이어 상태 UI
    public Action<Stat> OnHealthChanged;
    public Action<Stat> OnStaminaChanged;

    private void AddPlayerConditionActions()
    {
        OnHealthChanged += _playerCondition.UpdateHealthBar;
        OnStaminaChanged += _playerCondition.UpdateStaminaBar;
    }

    private void RemovePlayerConditionActions()
    {
        OnHealthChanged -= _playerCondition.UpdateHealthBar;
        OnStaminaChanged -= _playerCondition.UpdateStaminaBar;
    }
    #endregion

    #region 플레이어 상호작용 UI
    public Action<string, string> OnPromptChanged;
    public Action OnEndInteraction;

    private void AddPromptActions()
    {
        OnPromptChanged += HandlePromptChanged;
        OnEndInteraction += HandlePromptEndInteraction;
    }

    private void RemovePromptActions()
    {
        OnPromptChanged -= HandlePromptChanged;
        OnEndInteraction -= HandlePromptEndInteraction;
    }

    private void HandlePromptChanged(string name, string description)
    {
        _prompt.SetActiveText(true);
        _prompt.UpdateText(name, description);
    }

    private void HandlePromptEndInteraction()
    {
        _prompt.SetActiveText(false);
    }
    #endregion
}
