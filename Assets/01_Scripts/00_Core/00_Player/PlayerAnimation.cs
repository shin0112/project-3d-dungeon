using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        _animator.SetBool(Define.Player_Anim_Walk, true);
        _animator.SetBool(Define.Player_Anim_Dash, false);
    }

    public void Dash()
    {
        _animator.SetBool(Define.Player_Anim_Dash, true);
    }

    public void Idle()
    {
        _animator.SetBool(Define.Player_Anim_Walk, false);
        _animator.SetBool(Define.Player_Anim_Dash, false);
    }
}
