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
        UpdateUI?.Invoke(this);
    }

    public void UnNotifyUpdateUI()
    {
        UpdateUI = null;
    }

    public void AddValue(float value)
    {
        float minValue = Mathf.Min(Value + value, MaxValue);
        //Logger.Log($"값 변경 {Value} ▶ {minValue}");
        Value = minValue;

        UpdateUI?.Invoke(this);
    }

    public void SubstactValue(float value)
    {
        float maxValue = Mathf.Max(Value - value, 0);
        //Logger.Log($"값 변경 {Value} ▶ {maxValue}");
        Value = maxValue;

        UpdateUI?.Invoke(this);
    }

    public void IncreaseMaxValue(float value)
    {
        MaxValue += value;
        UpdateUI?.Invoke(this);
    }

    public void DecreaseMaxValue(float value)
    {
        if (MaxValue < value)
        {
            Logger.Log("최대치보다 큰 값을 줄일 수 없음");
            return;
        }
        MaxValue -= value;
        Value = MathF.Min(MaxValue, Value);
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
    public bool HasStaminaBuff { get; set; } = false;

    private float _stopFillTime = 5f;
    private bool _isStopFill = false;
    #endregion

    private void Start()
    {
        // todo: 값 저장해서 불러오기
        _health = new Stat(100, 100);
        _stamina = new Stat(30, 100);

        _health.NotifyUpdateUI(UIManager.Instance.OnHealthChanged);
        _stamina.NotifyUpdateUI(UIManager.Instance.OnStaminaChanged);
    }

    private void Update()
    {
        if (_isStopFill && !HasStaminaBuff) return;

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
        if (!HasStaminaBuff) _stamina.SubstactValue(valuae);

        if (_stamina.Value < 1f)
        {
            _isStopFill = true;
            Logger.Log("스테미나 회복 일시정지");
            Invoke("ResetStopFill", _stopFillTime);
        }
    }

    public void IncreaseMaxHealth(float value)
    {
        _health.IncreaseMaxValue(value);
    }

    public void DecreaseMaxHealth(float value)
    {
        _health.DecreaseMaxValue(value);
    }
    public void IncreaseMaxStamina(float value)
    {
        _stamina.IncreaseMaxValue(value);
    }

    public void DecreaseMaxStamina(float value)
    {
        _stamina.DecreaseMaxValue(value);
    }

    private void ResetStopFill()
    {
        _isStopFill = false;
    }
}
