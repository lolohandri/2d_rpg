using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    float _timeToReborn = 3f;
    public PlayerDeadState(PlayerContext ctx, PlayerStateFactory factory) : base(ctx, factory) { }

    public override void EnterState()
    {
        _ctx.Animator.SetBool("IsDead", _ctx.IsDead);
        _ctx.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public override void UpdateState()
    {
        _timeToReborn -= Time.deltaTime;
        if(_timeToReborn <= 0)
        {
            _ctx.CurrentHealth = _ctx.MaxHealth;
            _ctx.HealthBar.SetHealth(_ctx.MaxHealth);
            _ctx.transform.position = _ctx.SpawnTransform.position;
            _timeToReborn = Random.Range(3f, 5f);
            CheckSwitchStates();
        } 
    }
    public override void CheckSwitchStates()
    {
        SwitchState(_factory.Idle());
    }


    public override void OnCollisionEnter2D()
    {
        throw new System.NotImplementedException();
    }

}
