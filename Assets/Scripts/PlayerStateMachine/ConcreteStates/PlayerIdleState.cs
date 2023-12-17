using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerContext ctx, PlayerStateFactory factory) : base(ctx, factory) { }

    public override void EnterState()
    {
        _ctx.IsDead = false;
        _ctx.Animator.SetBool("IsDead", _ctx.IsDead);
    }
    public override void UpdateState()
    {
        _ctx.HorizontalInput = Input.GetAxisRaw("Horizontal");
        _ctx.VerticalInput = Input.GetAxisRaw("Vertical");
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_ctx.IsWalking)
        {
            SwitchState(_factory.Walk());
        }else if (Input.GetMouseButtonDown(0))
        {
            SwitchState(_factory.Attack());
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchState(_factory.Hurt());
        }
    }

    public override void OnCollisionEnter2D()
    {
        
    }

}
