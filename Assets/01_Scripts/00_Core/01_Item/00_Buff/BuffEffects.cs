using System.Collections;
using UnityEngine;

public class BuffEffects : MonoBehaviour
{
    private PlayerCondition _playerCondition;
    private PlayerController _playerController;

    private void Start()
    {
        _playerCondition = Managers.Instance.Game.Player.PlayerCondition;
        _playerController = Managers.Instance.Game.Player.PlayerController;
    }

    public void ApplyEffect(ItemData item)
    {
        switch (item.Buff.Type)
        {
            case BuffType.InfiniteStamina:
                ApplyInfiniteStamina(item.Buff.Value);
                break;
            case BuffType.SpeedBoost:
                ApplySpeedBoost(item.Buff.Value);
                break;
            case BuffType.HighJump:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 스테미나 무한
    /// </summary>
    private void ApplyInfiniteStamina(float value)
    {
        StartCoroutine(InfiniteStaminaCoroutine(value));
    }

    private IEnumerator InfiniteStaminaCoroutine(float value)
    {
        _playerCondition.HasStaminaBuff = true;
        yield return new WaitForSeconds(value);
        _playerCondition.HasStaminaBuff = false;
    }

    private void ApplySpeedBoost(float value)
    {
        StartCoroutine(SpeedBoostCoroutine(value));
    }

    private IEnumerator SpeedBoostCoroutine(float value)
    {
        _playerController.BuffSpped(1.3f);
        yield return new WaitForSeconds(value);
        _playerController.ResetSpeed(1.3f);
    }
}
