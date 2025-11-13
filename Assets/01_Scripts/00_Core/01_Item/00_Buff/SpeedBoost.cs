using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour, IConsumable
{
    private WaitForSeconds _duration = new WaitForSeconds(Define.Item_Buff_Duration_SpeedBoost);

    public void ApplyEffect()
    {
        var pc = GameManager.Instance.Player.PlayerCondition;

        StartCoroutine(StaminaBoostCoroutine(pc));
    }

    private IEnumerator StaminaBoostCoroutine(PlayerCondition playerCondition)
    {
        playerCondition.HasStaminaBuff = true;
        yield return _duration;
        playerCondition.HasStaminaBuff = false;
        Destroy(gameObject);
    }
}
