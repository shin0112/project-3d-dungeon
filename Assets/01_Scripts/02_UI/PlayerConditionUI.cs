using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _staminaBar;

    public void UpdateHealthBar(Stat health)
    {
        _healthBar.fillAmount = health.Value / health.MaxValue;
    }

    public void UpdateStaminaBar(Stat stamina)
    {
        _staminaBar.fillAmount = stamina.Value / stamina.MaxValue;
    }
}
