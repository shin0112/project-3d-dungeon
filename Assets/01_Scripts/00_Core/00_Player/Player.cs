using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerCondition _condition;

    public PlayerController PlayerController => _controller;
    public PlayerCondition PlayerCondition => _condition;

    [Header("Item")]
    [SerializeField] private Transform _dropPosition;
    public Transform DropPosition => _dropPosition;
    public ItemData ItemData { get; set; }
    public Action AddBuffItem;
    public Action UseBuffItem;

    [Header("Equipment")]
    [SerializeField] private GameObject _hat;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _condition = GetComponent<PlayerCondition>();

        EnrollActions();
    }

    private void OnDestroy()
    {
        DeleteActions();
    }

    #region 플레이어 움직임 & 상태 관리 및 상호작용
    private void EnrollActions()
    {
        _controller.OnDash += HandleDash;
        _controller.OnClimb += HandleClimb;
    }

    private void DeleteActions()
    {
        _controller.OnDash -= HandleDash;
        _controller.OnClimb -= HandleClimb;
    }

    enum StaminaMode
    {
        Dash,
        Climb
    }

    private Dictionary<StaminaMode, float> _staminaRate = new Dictionary<StaminaMode, float> {
        { StaminaMode.Dash, Define.Player_Stamina_DashConsumeRate },
        { StaminaMode.Climb, Define.Player_Stamina_ClimbConsumeRate }
    };

    private void HandleUseStamina(StaminaMode mode)
    {
        _controller.CanSpendStamina = _condition.CurStamina >= 1 || _condition.HasStaminaBuff;
        if (_controller.CanSpendStamina)
        {
            _condition.UseStamina(Time.fixedDeltaTime * _staminaRate[mode]);
            Logger.Log("스테미나 사용");
        }
        else
        {
            Logger.Log("스테미나 부족");
        }
    }

    private void HandleDash()
    {
        HandleUseStamina(StaminaMode.Dash);
    }

    private void HandleClimb()
    {
        HandleUseStamina(StaminaMode.Climb);
    }
    #endregion

    #region 플레이어 상태 변경
    public void Heal(float value) => _condition.Heal(value);

    public void Damage(float value) => _condition.Damage(value);
    #endregion

    #region 플레이어 장비
    public void EquipHat(ItemData data)
    {
        Instantiate(data.DropPrefab, _hat.transform);
    }

    public void UnEquipHat()
    {

    }
    #endregion
}
