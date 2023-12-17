public abstract class PlayerBaseState
{
    protected PlayerContext _ctx;
    protected PlayerStateFactory _factory;

    public PlayerBaseState(PlayerContext ctx, PlayerStateFactory factory)
    {
        _ctx = ctx;
        _factory = factory;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void CheckSwitchStates();
    public abstract void OnCollisionEnter2D();

    protected void SwitchState(PlayerBaseState newState)
    {
        newState.EnterState();
        _ctx.CurrentState = newState;
    }
}
