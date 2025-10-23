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

    [Header("ATTACK STATE")]
    [SerializeField] private string attackAnimation; 

    [Header("DEATH STATE")]
    [SerializeField] private string deathAnimation;


    #region IDLE
    public override void EnterIdle()
    {
        anim.Play(idleAnimationName);
        idleStateTimer = Random.Range(minIdleTime, minIdleTime);
        patrollPhysics.NegateForces();
    }

    public override void UpdateIdle()
    {
        if (patrollPhysics.playerBehind)
        {
            ForceFlip();
            speed *= -1;
            turnCooldown = minimumTurnDelay;
            ChangeState(EnemyState.Move);
        }

        idleStateTimer -= Time.deltaTime;
        if (idleStateTimer <= 0 || patrollPhysics.playerAhead)
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
        if (moveStateTimer <= 0 && patrollPhysics.playerAhead == false)
            ChangeState(EnemyState.Idle);

        if (turnCooldown > 0)
            turnCooldown -= Time.deltaTime;

        if(patrollPhysics.playerBehind && turnCooldown <= 0)
        {
            ForceFlip();
            speed *= -1;
            turnCooldown = minimumTurnDelay;
            return;
        }

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
        patrollPhysics.canCheckBehind = false;
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
        StartCoroutine(CheckBehindDelay());
    }

    IEnumerator CheckBehindDelay()
    {
        yield return new WaitForSeconds(0.3f);
        patrollPhysics.canCheckBehind = true;
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

    #region DEATH
    public override void EnterDeath()
    {
        anim.Play(deathAnimation);
        patrollPhysics.DeathColliderDeactivation();
        patrollPhysics.NegateForces();
    }
    #endregion
}
