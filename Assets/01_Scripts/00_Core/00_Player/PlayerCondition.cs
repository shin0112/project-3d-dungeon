using System;
using UnityEngine;

public class Stat
{
    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public Action<Stat> UpdateUI { get; private set; }

    public Stat(float value, float max)
    {
        Value = value;
        MaxValue = max;
    }

    public void NotifyUpdateUI(Action<Stat> updateUI)
    {
        UpdateUI = updateUI;
    }

    public void UnNotifyUpdateUI()
    {
        UpdateUI = null;
    }

    public void AddValue(float value)
    {
        float minValue = Mathf.Min(Value + value, MaxValue);
        Logger.Log($"값 변경 {Value} ▶ {minValue}");
        Value = minValue;

        UpdateUI?.Invoke(this);
    }

    public void SubstactValue(float value)
    {
        float maxValue = Mathf.Max(Value - value, 0);
        Logger.Log($"값 변경 {Value} ▶ {maxValue}");
        Value = maxValue;

        UpdateUI?.Invoke(this);
    }
}


public class PlayerCondition : MonoBehaviour
{
    #region 변수
    private Stat _health;
    private Stat _stamina;

    public float CurHealth => _health.Value;
    public float CurStamina => _stamina.Value;
    public bool BlockSpendStamina { get; set; } = false;
    #endregion

    private void Start()
    {
        // todo: 값 저장해서 불러오기
        _health = new Stat(100, 100);
        _stamina = new Stat(300, 300);

        _health.NotifyUpdateUI(UIManager.Instance.OnHealthChanged);
        _stamina.NotifyUpdateUI(UIManager.Instance.OnStaminaChanged);
    }

    private void Update()
    {
        _stamina.AddValue(Time.deltaTime * Define.Player_Stamina_AutoRecoveryRate);
    }

    private void OnDestroy()
    {
        _health.UnNotifyUpdateUI();
        _stamina.UnNotifyUpdateUI();
    }

    public void Heal(float value)
    {
        _health.AddValue(value);
    }

    public void Damage(float value)
    {
        _health.SubstactValue(value);
    }

    public void UseStamina(float valuae)
    {
        if (!BlockSpendStamina) _stamina.SubstactValue(valuae);
    }
}
