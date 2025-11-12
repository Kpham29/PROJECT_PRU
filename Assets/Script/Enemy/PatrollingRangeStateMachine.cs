using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingRangeStateMachine : EnemyStateMachine
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
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float rayLength;
    [SerializeField] private float damage;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float visibleLineTime;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private LayerMask whatToHit;

    [Header("DEATH STATE")]
    [SerializeField] private string deathAnimation;

    [Header("HURT STATE")]
    [SerializeField] private string hurtAnimation;


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
        if (idleStateTimer <= 0 || patrollPhysics.playerAhead==false)
            ChangeState(EnemyState.Move);

        if (patrollPhysics.playerAhead)
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

        if (patrollPhysics.playerBehind && turnCooldown <= 0)
        {
            ForceFlip();
            speed *= -1;
            turnCooldown = minimumTurnDelay;
            return;
        }

        if (patrollPhysics.wallDetected || patrollPhysics.groundDetected == false)
        {
            if (turnCooldown > 0)
                return;
            ForceFlip();
            speed *= -1;
            turnCooldown = minimumTurnDelay;
        }

        if (patrollPhysics.playerAhead)
            ChangeState(EnemyState.Attack);
    }

    public override void FixUpdateMove()
    {
        patrollPhysics.rb.velocity = new Vector2(speed, patrollPhysics.rb.velocity.y);
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
        if (patrollPhysics.playerAhead)
        {
            //Reset Đòn đánh
            anim.Play(attackAnimation, 0, 0);
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

    public void ShootAttack()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(shootingPoint.position,-transform.right,rayLength,whatToHit);
        lineRenderer.positionCount = 2;
        if (hitInfo)
        {
            lineRenderer.SetPosition(0, shootingPoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            CharacterStat characterStat =  hitInfo.collider.GetComponent<CharacterStat>();
            if(characterStat != null)
            {
                characterStat.TakeDamage((int)damage);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, shootingPoint.position);
            lineRenderer.SetPosition(1, shootingPoint.position + -transform.right*20);
        }
        StartCoroutine(ResetShootLine());
    }

    private IEnumerator ResetShootLine()
    {
        yield return new WaitForSeconds(visibleLineTime);
        lineRenderer.positionCount = 0;
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

    #region HURT
    public override void EnterHurt()
    {
        anim.Play(hurtAnimation);
        patrollPhysics.NegateForces();
    }

    public void EndOfHurt()
    {
        if (previousState == EnemyState.Attack)
        {
            ChangeState(EnemyState.Idle);
        }
        else if (previousState == EnemyState.Move)
        {
            ChangeState(EnemyState.Move);
        }
        else
        {
            ChangeState(EnemyState.Idle);
        }
    }
    #endregion
}
