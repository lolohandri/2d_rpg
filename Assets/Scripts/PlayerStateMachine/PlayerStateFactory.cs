using System.Collections.Generic;

public class PlayerStateFactory
{
    readonly PlayerContext _context;
    readonly Dictionary<string, PlayerBaseState> _states = new();
    public PlayerStateFactory(PlayerContext context)
    {
        _context = context;
        _states["idle"] = new PlayerIdleState(_context, this);
        _states["walk"] = new PlayerWalkState(_context, this);
        _states["attack"] = new PlayerAttackState(_context, this);
        _states["hurt"] = new PlayerHurtState(_context, this);
        _states["dead"] = new PlayerDeadState(_context, this);
    }

    public PlayerBaseState Idle()
    {
        return _states["idle"];
    }
    public PlayerBaseState Walk()
    {
        return _states["walk"];
    }
    public PlayerBaseState Attack()
    {
        return _states["attack"];
    }
    public PlayerBaseState Hurt()
    {
        return _states["hurt"];
    }
    public PlayerBaseState Dead()
    {
        return _states["dead"];
    }
}
