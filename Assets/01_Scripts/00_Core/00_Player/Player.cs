using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerCondition _condition;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _condition = GetComponent<PlayerCondition>();
    }

    private void Update()
    {
    }

    #region 플레이어 상태 변경
    public void Heal(float value) => _condition.Heal(value);
    public void Damage(float value) => _condition.Damage(value);
    #endregion
}
