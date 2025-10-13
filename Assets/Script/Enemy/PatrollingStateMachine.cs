using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingStateMachine : EnemyStateMachine
{
    [SerializeField] private PatrollPhysics patrollPhysics;

    [Header("IDLE STATE")]
    [SerializeField] private string idleAnimationName;
    [SerializeField] private float minIdleTime;
    [SerializeField] private float maxIdleTime;
    private float idleStateTimer;

    [Header("MOVE STATE")]
    [SerializeField] private string moveAnimationName;
    [SerializeField] private float speed;
    [SerializeField] private float minMoveTime;
    [SerializeField] private float maxMoveTime;
    [SerializeField] private float minimumTurnDelay;
    private float moveStateTimer;
    private float turnCooldown;

    [Header("MOVE STATE")]
    [SerializeField] private string attackAnimation;


    #region IDLE
    public override void EnterIdle()
    {
        anim.Play(idleAnimationName);
        idleStateTimer = Random.Range(minIdleTime, minIdleTime);
        patrollPhysics.NegateForces();
    }

    public override void UpdateIdle()
    {
        idleStateTimer -= Time.deltaTime;
        if (idleStateTimer <= 0)
            ChangeState(EnemyState.Move);

        if (patrollPhysics.inAttackRange)
            ChangeState(EnemyState.Attack);
    }

    public override void ExitIdle()
    {
        
    }
    #endregion

    #region MOVE
    public override void EnterMove()
    {
        anim.Play(moveAnimationName);
        moveStateTimer = Random.Range(minMoveTime, maxMoveTime);
    }

    public override void UpdateMove()
    {
        moveStateTimer -= Time.deltaTime;
        if (moveStateTimer <= 0)
            ChangeState(EnemyState.Idle);

        if (turnCooldown > 0)
            turnCooldown -= Time.deltaTime;

        if(patrollPhysics.wallDetected || patrollPhysics.groundDetected == false)
        {
            if (turnCooldown > 0)
                return;
            ForceFlip();
            speed *= -1;
            turnCooldown = minimumTurnDelay;
        }

        if (patrollPhysics.inAttackRange)
            ChangeState(EnemyState.Attack);
    }

    public override void FixUpdateMove()
    {
        patrollPhysics.rb.velocity = new Vector2(speed,patrollPhysics.rb.velocity.y);
    }
    #endregion

    #region ATTACK

    public override void EnterAttack()
    {
        anim.Play(attackAnimation);
        patrollPhysics.NegateForces();
    }

    public void EndOfAttack()
    {
        if (patrollPhysics.inAttackRange)
        {
            //Reset Đòn đánh
            anim.Play(attackAnimation,0,0);
        }
        else
        {
            ChangeState(previousState);
        }
        
    }

    public override void UpdateAttack()
    {
        base.UpdateAttack();
    }

    public override void FixUpdateAttack()
    {
        base.FixUpdateAttack();
    }

    #endregion
}
