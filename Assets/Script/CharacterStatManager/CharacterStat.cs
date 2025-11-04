using System.Collections;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [Header("Stat")]

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int damage = 10;

    public int currentHealth;
    protected Animator animator;
    public bool isDead = false;

    protected virtual void Start()

    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int dame)
    {
        if (isDead) return;

        currentHealth -= dame;
        if (currentHealth < 0)
            currentHealth = 0;


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

    public virtual void Attack(CharacterStat stat)
    {
        if (isDead) return;
        stat.TakeDamage(damage);
    }

    public virtual void RestoreHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Hurt");
    }
}
