using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _icon;

    public void SetActiveText(bool active)
    {
        _name.gameObject.SetActive(active);
        _description.gameObject.SetActive(active);
    }

    public void UpdateText(string name, string description)
    {
        _name.text = name;
        _description.text = description;
    }
}
