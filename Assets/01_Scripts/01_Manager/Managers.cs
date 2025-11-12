using UnityEngine;

public class Managers : MonoBehaviour
{
    public UIManager UI => UIManager.Instance;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        UI.Init(GetComponentInChildren<PlayerConditionUI>(),
                GetComponentInChildren<PromptUI>(),
                GetComponentInChildren<InventoryUI>());
    }
}
