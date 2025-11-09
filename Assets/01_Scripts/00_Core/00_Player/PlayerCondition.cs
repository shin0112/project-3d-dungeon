using UnityEngine;

public class Stat
{
    public float Value { get; private set; }
    public float MaxValue { get; private set; }

    public Stat(float value, float max)
    {
        Value = value;
        MaxValue = max;
    }

    public Stat AddValue(float value)
    {
        float minValue = Mathf.Min(Value + value, MaxValue);
        Logger.Log($"값 변경 {Value} ▶ {minValue}");
        Value = minValue;

        return this;
    }

    public Stat SubstactValue(float value)
    {
        float maxValue = Mathf.Max(Value - value, 0);
        Logger.Log($"값 변경 {Value} ▶ {maxValue}");
        Value = maxValue;

        return this;
    }
}


public class PlayerCondition : MonoBehaviour
{
    #region 변수
    private Stat _health;
    private Stat _stamina;

    public Stat Health
    {
        get { return _health; }
        set
        {
            _health = value;
            UIManager.Instance.OnHealthChanged?.Invoke(_health);
        }
    }
    #endregion

    private void Awake()
    {
        // todo: 값 저장해서 불러오기
        _health = new Stat(100, 100);
        _stamina = new Stat(300, 300);
    }

    public void Heal(float value)
    {
        Health = _health.AddValue(value);
    }

    public void Damage(float value)
    {
        Health = _health.SubstactValue(value);
    }
}
