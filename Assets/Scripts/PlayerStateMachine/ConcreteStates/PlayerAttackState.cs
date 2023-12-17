using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    float _attackRate = 0.5f;
    public PlayerAttackState(PlayerContext ctx, PlayerStateFactory factory) : base(ctx, factory){}
    public override void EnterState()
    {
        _ctx.Animator.SetTrigger("Attack");
    }
    public override void UpdateState()
    {
        _attackRate -= Time.deltaTime;
        if (_attackRate <= 0)
        {
            Collider2D[] enemiesCollider = Physics2D.OverlapCircleAll((Vector2)_ctx.AttackPoint.position, _ctx.attackRange, _ctx.EnemyLayer);
            foreach (var enemy in enemiesCollider)
            {
                enemy.GetComponentInParent<EnemyContext>().TakeDamage(_ctx.attackDamage);
            }
            _attackRate = 0.5f;
            CheckSwitchStates();
        }

    }
    public override void CheckSwitchStates()
    {

        if (!_ctx.IsWalking)
        {
            SwitchState(_factory.Idle());
        }
        else if (_ctx.IsWalking)
        {
            SwitchState(_factory.Walk());
        }
    }
    public override void OnCollisionEnter2D()
    {
        throw new System.NotImplementedException();
    }

}
