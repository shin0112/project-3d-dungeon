using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("Player UI")]
    [SerializeField] private PlayerConditionUI _playerCondition;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (_playerCondition == null && !TryGetComponent<PlayerConditionUI>(out _playerCondition))
        {
            Logger.Log("player condition ui is null");
        }

        SetPlayerConditionActions();
    }

    #region 플레이어 상태 UI
    public Action<Stat> OnHealthChanged;
    public Action<Stat> OnStaminaChanged;

    private void SetPlayerConditionActions()
    {
        OnHealthChanged += _playerCondition.UpdateHealthBar;
        OnStaminaChanged += _playerCondition.UpdateStaminaBar;
    }
    #endregion
}
