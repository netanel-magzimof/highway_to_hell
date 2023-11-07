using System.Linq;

public class PlayerStateManager
{
    #region Fields

    private static readonly PlayerState[] _runRestrictions = new[] { PlayerState.Attack, PlayerState.Dash, PlayerState.Fall };
    private static readonly PlayerState[] _dashRestrictions = new[] { PlayerState.Attack, PlayerState.Fall};
    private static readonly PlayerState[] _attackRestrictions = new[] { PlayerState.Dash, PlayerState.Fall};

    private PlayerState _playerState;
    
    #endregion

    #region Methods

    public bool CanRunFromState()
    {
        return !_runRestrictions.Contains(_playerState);
    }
    
    public bool CanDashFromState()
    {
        return !_dashRestrictions.Contains(_playerState);
    }
    
    public bool CanAttackFromState()
    {
        return !_attackRestrictions.Contains(_playerState);
    }

    public PlayerState GetPlayerState()
    {
        return _playerState;
    }

    public void SetPlayerState(PlayerState playerState)
    {
        _playerState = playerState;
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