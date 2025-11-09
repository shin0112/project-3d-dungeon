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

    public void AddValue(float value)
    {
        float minValue = Mathf.Min(Value + value, MaxValue);
        Logger.Log($"값 변경 {Value} ▶ {minValue}");
        Value = minValue;
    }

    public void SubstactValue(float value)
    {
        float maxValue = Mathf.Max(Value - value, 0);
        Logger.Log($"값 변경 {Value} ▶ {maxValue}");
        Value = maxValue;
    }
}


public class PlayerCondition : MonoBehaviour
{
    #region 변수
    private Stat _hp;
    private Stat _stamina;
    #endregion

    private void Awake()
    {
        // todo: 값 저장해서 불러오기
        _hp = new Stat(100, 100);
        _stamina = new Stat(300, 300);
    }
}
