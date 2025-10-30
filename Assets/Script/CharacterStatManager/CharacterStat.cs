using System.Collections;
using System.Collections.Generic;
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
        currentHealth -= dame;

        if (currentHealth <= 0)
        {
            isDead = true;
            StartCoroutine(RemoveAfterAni());
            animator.SetTrigger("Die");

            
            // Play death sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerDeath();

        }
        else
        {
            animator.SetTrigger("Hurt");

            
            // Play hurt sound
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayPlayerHurt();

        }
    }

    public void Attack(CharacterStat stat)
    {
        if (isDead) return;
        stat.TakeDamage(damage);
    }

    private IEnumerator RemoveAfterAni()
    {
        float length = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(length);

        Destroy(gameObject);
    }
}
