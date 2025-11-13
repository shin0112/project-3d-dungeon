using System.Collections;
using UnityEngine;

public class BuffEffects : MonoBehaviour
{
    public void ApplyEffect(ItemData item)
    {
        switch (item.Buff.Type)
        {
            case BuffType.InfiniteStamina:
                ApplyInfiniteStamina();
                break;
            case BuffType.SpeedBoost:
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
    private WaitForSeconds _duration = new WaitForSeconds(Define.Item_Buff_Duration_SpeedBoost);

    public void ApplyInfiniteStamina()
    {
        var pc = GameManager.Instance.Player.PlayerCondition;

        StartCoroutine(InfiniteStaminaCoroutine(pc));
    }

    private IEnumerator InfiniteStaminaCoroutine(PlayerCondition playerCondition)
    {
        playerCondition.HasStaminaBuff = true;
        yield return _duration;
        playerCondition.HasStaminaBuff = false;
    }
}
