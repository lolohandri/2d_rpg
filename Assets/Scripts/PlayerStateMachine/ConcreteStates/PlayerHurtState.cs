public class PlayerHurtState : PlayerBaseState
{
    public PlayerHurtState(PlayerContext ctx, PlayerStateFactory factory) : base(ctx, factory) { }

    public override void EnterState()
    {
        _ctx.TakeDamage(20);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void CheckSwitchStates()
    {
        if (_ctx.IsWalking)
        {
            SwitchState(_factory.Walk());
        }else if (!_ctx.IsWalking)
        {
            SwitchState(_factory.Idle());
        }else if (_ctx.IsDead)
        {
            SwitchState(_factory.Dead());
        }
    }

    public override void OnCollisionEnter2D()
    {
        throw new System.NotImplementedException();
    }
}
