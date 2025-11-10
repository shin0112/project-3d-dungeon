using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData _data;
    public string[] GetInteractPrompt()
    {
        return new string[] { _data.Name, _data.Description };
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
