using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PromptMode
{
    General,
    Buff,
}

public class PromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _tip;
    [SerializeField] private Image _icon;
    private PromptMode _mode = PromptMode.General;
    public PromptMode Mode { get; set; }

    public void SetActiveText(bool active)
    {
        _tip.gameObject.SetActive(_mode != PromptMode.Buff && active);
        _name.gameObject.SetActive(active);
        _description.gameObject.SetActive(active);
    }

    public void UpdateText(string name, string description)
    {
        _name.text = name;
        _description.text = description;
    }
}
