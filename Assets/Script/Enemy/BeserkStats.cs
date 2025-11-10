using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeserkStats : EnemyStats
{
    [SerializeField] protected EnemyStateMachine enemyStateMachine;

    [SerializeField] private bool isBoss = true;

    private void Awake()
    {
        // Disable default enemy sounds for boss
        if (isBoss)
            playDefaultSounds = false;
    }

    private void Start()
    {
        // Play boss roar when boss appears
        if (isBoss && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossRoar();
            AudioManager.Instance.PlayBossMusic();
        }
    }

    protected override void DamageProcess()
    {
        // Override to play boss hurt sound instead of regular enemy hurt
        if (isBoss && AudioManager.Instance != null)
            AudioManager.Instance.PlayBossHurt();

        // ✅ Gọi base method (của EnemyStats)
        base.DamageProcess();
    }

    protected override void DeathProcess()
    {
        enemyStateMachine.ChangeState(EnemyStateMachine.EnemyState.Death);

        // Play boss death sound and return to background music
        if (isBoss && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossDeath();
            StartCoroutine(ReturnToBackgroundMusic());
        }
    }

    protected override void HurtProcess()
    {
        enemyStateMachine.ChangeState(EnemyStateMachine.EnemyState.Hurt);
    }

    private IEnumerator ReturnToBackgroundMusic()
    {
        yield return new WaitForSeconds(2f);
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayBackgroundMusic();
    }
}
