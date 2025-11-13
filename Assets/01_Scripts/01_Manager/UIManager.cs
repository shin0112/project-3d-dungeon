using System;

public class UIManager
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) _instance = new();
            return _instance;
        }
    }

    private UIManager() { }

    private PlayerConditionUI _playerCondition;
    private PromptUI _prompt;
    private InventoryUI _inventory;
    public PromptUI Prompt => _prompt;
    public InventoryUI Inventory => _inventory;

    public void Init(
        PlayerConditionUI playerCondition,
        PromptUI prompt,
        InventoryUI inventory)
    {
        _playerCondition = playerCondition;
        _prompt = prompt;
        _inventory = inventory;

        AddPlayerConditionActions();
        AddPromptActions();
    }

    public void OnDestroy()
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
