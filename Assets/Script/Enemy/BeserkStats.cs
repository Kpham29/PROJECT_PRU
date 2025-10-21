using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeserkStats : EnemyStats
{
    [SerializeField] protected EnemyStateMachine enemyStateMachine;
    protected override void DamageProcess()
    {
         
        base.DamageProcess();
    }

    protected override void DeathProcess()
    {
        enemyStateMachine.ChangeState(EnemyStateMachine.EnemyState.Death);
    }
}
