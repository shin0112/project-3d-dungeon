using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    public void UpdateText(string name, string description)
    {
        _name.text = name;
        _description.text = description;
    }
}
