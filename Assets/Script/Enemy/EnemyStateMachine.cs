using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    protected EnemyState previousState;
    protected EnemyState currentState;

    [SerializeField] protected Animator anim;
    public bool facingRight = true;
    public void ForceFlip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Death,
        Hurt
    }

    public void ChangeState(EnemyState newState)
    {
        if (newState == currentState)
            return;
        ExitState(currentState);
        previousState = currentState;
        currentState = newState;
        EnterState(currentState);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Move:
                UpdateMove();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
            case EnemyState.Death:
                UpdateDeath();
                break;
            case EnemyState.Hurt:
                UpdateHurt();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                FixUpdateIdle();
                break;
            case EnemyState.Move:
                FixUpdateMove();
                break;
            case EnemyState.Attack:
                FixUpdateAttack();
                break;
            case EnemyState.Death:
                FixUpdateDeath();
                break;
            case EnemyState.Hurt:
                FixUpdateHurt();
                break;
        }
    }

    protected void EnterState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                EnterIdle();
                break;
            case EnemyState.Move:
                EnterMove();
                break;
            case EnemyState.Attack:
                EnterAttack();
                break;
            case EnemyState.Death:
                EnterDeath();
                break;
            case EnemyState.Hurt:
                EnterHurt();
                break;
        }
    }

    protected void ExitState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                ExitIdle();
                break;
            case EnemyState.Move:
                ExitMove();
                break;
            case EnemyState.Attack:
                ExitAttack();
                break;
            case EnemyState.Death:
                ExitDeath();
                break;
            case EnemyState.Hurt:
                ExitHurt();
                break;
        }
    }

    public virtual void EnterIdle()
    {

    }
    public virtual void EnterMove()
    {

    }
    public virtual void EnterAttack()
    {

    }
    public virtual void EnterDeath()
    {

    }
    public virtual void EnterHurt()
    {

    }


    public virtual void ExitIdle()
    {

    }
    public virtual void ExitMove()
    {

    }
    public virtual void ExitAttack()
    {

    }
    public virtual void ExitDeath()
    {

    }
    public virtual void ExitHurt()
    {

    }


    public virtual void UpdateIdle()
    {

    }
    public virtual void UpdateMove()
    {

    }
    public virtual void UpdateAttack()
    {

    }
    public virtual void UpdateDeath()
    {

    }
    public virtual void UpdateHurt()
    {

    }

    public virtual void FixUpdateIdle()
    {

    }
    public virtual void FixUpdateMove()
    {

    }
    public virtual void FixUpdateAttack()
    {

    }
    public virtual void FixUpdateDeath()
    {

    }
    public virtual void FixUpdateHurt()
    {

    }

}
