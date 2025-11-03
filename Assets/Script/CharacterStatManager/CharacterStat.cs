using System.Collections;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] public int maxHealth;
    [SerializeField] public int damage;
    private int currentHealth;
    private Animator animator;
    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int dame)
    {
        if (isDead) return;
        currentHealth -= dame;

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Die");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerDeath();
        }
        else
        {
            animator.SetTrigger("Hurt");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerHurt();
        }
    }

    public void Attack(CharacterStat stat)
    {
        if (isDead) return;
        stat.TakeDamage(damage);
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Hurt");
    }
}