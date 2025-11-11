using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerCondition _condition;

    public PlayerController PlayerController => _controller;
    public PlayerCondition PlayerCondition => _condition;

    [Header("Item")]
    [SerializeField] private Transform _dropPosition;
    public ItemData ItemData { get; set; }
    public Action AddBuffItem;
    public Action UseBuffItem;

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

    private void EnrollActions()
    {
        _controller.OnDash += HandleDash;
    }

    private void DeleteActions()
    {
        _controller.OnDash -= HandleDash;
    }

    private void HandleDash()
    {
        _controller.CanSpendStamina = _condition.CurStamina > 0;
        if (_controller.CanSpendStamina)
        {
            _condition.UseStamina(Time.deltaTime * Define.Player_Stamina_DashConsumeRate);
        }
    }

    #region 플레이어 상태 변경
    public void Heal(float value) => _condition.Heal(value);

    public void Damage(float value) => _condition.Damage(value);
    #endregion
}
