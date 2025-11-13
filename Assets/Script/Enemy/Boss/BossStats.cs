using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 500;
    public int currentHealth;
    public int damage = 50;

    [Header("UI")]
    public HealthBar healthBar; // Kéo Canvas-Health vào đây

    [SerializeField] protected BossStateMachine enemyStateMachine;
    [SerializeField] private bool isBoss = true;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (isBoss && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossRoar();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthBar();

        if (currentHealth <= 0)
            enemyStateMachine.ChangeState(BossStateMachine.BossState.Death);
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        enemyStateMachine.ChangeState(BossStateMachine.BossState.Death);
        if (isBoss && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossDeath();
        }
    }
}
