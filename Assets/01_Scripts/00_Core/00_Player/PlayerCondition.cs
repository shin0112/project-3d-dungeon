using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public Action<Stat> UpdateUI { get; private set; }

    public Stat(float value, float max, Action<Stat> action)
    {
        Value = value;
        MaxValue = max;
        UpdateUI = action;
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
    #endregion

    private void Start()
    {
        // todo: 값 저장해서 불러오기
        _health = new Stat(100, 100, UIManager.Instance.OnHealthChanged);
        _stamina = new Stat(300, 300, UIManager.Instance.OnStaminaChanged);
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
        _stamina.SubstactValue(valuae);
    }

    internal void UseStamina(object dashStaminaValue)
    {
        throw new NotImplementedException();
    }
}
