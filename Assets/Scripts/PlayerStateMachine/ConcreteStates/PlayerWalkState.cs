using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerContext ctx, PlayerStateFactory factory) : base(ctx, factory){ }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        HandleMoving();
        _ctx.Animator.SetBool("IsWalking", _ctx.IsWalking);
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (!_ctx.IsWalking)
        {
            SwitchState(_factory.Idle());
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
        throw new System.NotImplementedException();
    }

    private void HandleMoving()
    {
        _ctx.HorizontalInput = Input.GetAxisRaw("Horizontal");
        _ctx.VerticalInput = Input.GetAxisRaw("Vertical");
        _ctx.Rb2d.velocity = new Vector2(_ctx.HorizontalInput * _ctx.Speed, _ctx.VerticalInput * _ctx.Speed);
        Flip();
    }
    private void Flip()
    {
        if (_ctx.IsFacingRight && _ctx.HorizontalInput < 0f || !_ctx.IsFacingRight && _ctx.HorizontalInput > 0f)
        {
            _ctx.IsFacingRight = !_ctx.IsFacingRight;
            var localScale = _ctx.transform.localScale;
            localScale.x *= -1;
            _ctx.transform.localScale = localScale;
        }
    }
}
