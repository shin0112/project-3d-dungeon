public static class Define
{
    #region 플레이어 상태
    // speed
    public const float Player_MoveSpeed = 5f;
    public const float Player_DashSpeed = 10f;
    public const float Player_ClimbSpeed = 3f;

    // stamina
    public const float Player_Stamina_AutoRecoveryRate = 5f;
    public const float Player_Stamina_DashConsumeRate = 25f;
    public const float Player_Stamina_ClimbConsumeRate = 35f;

    // animator
    public const string Player_Anim_Walk = "Move";
    public const string Player_Anim_Dash = "Dash";
    public const string Player_Anim_Climb = "Climb";

    #endregion

    #region 버프 아이템
    public const float Item_Buff_Radius = 0.15f;
    public const float Item_Buff_JumpPower = 5f;

    public const float Item_Buff_Duration = 5f;
    #endregion
}
