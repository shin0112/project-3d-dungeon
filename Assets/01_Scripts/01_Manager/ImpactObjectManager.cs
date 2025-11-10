using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImpactObjectManager : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private List<IImpactable> _impactables = new();

    private void Start()
    {
        _impactables.Clear();
        _impactables = GetComponentsInChildren<IImpactable>().ToList();

        foreach (var impactable in _impactables)
        {
            impactable.Init(GameManager.Instance.Player.PlayerController, _layerMask);
        }

        Logger.Log($"impactable 리스트 초기화 ({_impactables.Count})");
    }
}
