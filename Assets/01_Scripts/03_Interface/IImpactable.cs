using UnityEngine;

public interface IImpactable
{
    public void Init(PlayerController controller, LayerMask layerMask);
    public void ModifyMovement();
}
