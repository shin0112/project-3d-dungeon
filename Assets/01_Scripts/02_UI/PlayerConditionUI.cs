using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _staminaBar;
    [SerializeField] private TextMeshProUGUI _healthValue;
    [SerializeField] private TextMeshProUGUI _staminaValue;

    public void UpdateHealthBar(Stat health)
    {
        _healthBar.fillAmount = health.Value / health.MaxValue;
        _healthValue.text = $"{(int)health.Value} / {(int)health.MaxValue}";
    }

    public void UpdateStaminaBar(Stat stamina)
    {
        _staminaBar.fillAmount = stamina.Value / stamina.MaxValue;
        _staminaValue.text = $"{(int)stamina.Value} / {(int)stamina.MaxValue}";
    }
}
