using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeserkStats : MonoBehaviour  // KHÔNG CẦN KẾ THỪA GÌ CẢ!
{
    [Header("Stats")]
    public int maxHealth = 500;
    public int currentHealth;
    public int damage = 50;

    [Header("UI")]
    public HealthBar healthBar; // Kéo Canvas-Health vào đây

    [SerializeField] protected EnemyStateMachine enemyStateMachine;
    [SerializeField] private bool isBoss = true;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // Dòng 1: hiển thị full máu

        // Boss roar only, no music change (scene music continues)
        if (isBoss && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossRoar();
            // Removed: AudioManager.Instance.PlayBossMusic();
            // Scene music will continue playing
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthBar(); // Dòng 2: cập nhật thanh máu

        if (currentHealth <= 0)
            Die();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        enemyStateMachine.ChangeState(EnemyStateMachine.EnemyState.Death);
        if (isBoss && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBossDeath();
            // Removed: StartCoroutine(ReturnToBackgroundMusic());
            // Scene music will continue playing, no need to switch
        }
    }
}