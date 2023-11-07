using System.Linq;

public static class PlayerStateManager
{
    #region Fields

    private static readonly PlayerState[] _runRestrictions = new[] { PlayerState.Attack, PlayerState.Dash, PlayerState.Fall };
    private static readonly PlayerState[] _dashRestrictions = new[] { PlayerState.Attack, PlayerState.Fall};
    private static readonly PlayerState[] _attackRestrictions = new[] { PlayerState.Dash, PlayerState.Fall};
    
    #endregion

    #region Methods

    public static bool CanRunFromState(PlayerState playerState)
    {
        return !_runRestrictions.Contains(playerState);
    }
    
    public static bool CanDashFromState(PlayerState playerState)
    {
        return !_dashRestrictions.Contains(playerState);
    }
    
    public static bool CanAttackFromState(PlayerState playerState)
    {
        return !_attackRestrictions.Contains(playerState);
    }
    
    #endregion
    
}

public enum PlayerState 
{
    Idle,
    Run,
    Dash,
    Fall,
    Attack,
    Stunned //TODO check if need stunned state
}