//using System.Collections;
//using UnityEngine;

//public class CharacterStat : MonoBehaviour
//{
//    [Header("Stat")]
//    [SerializeField] public int maxHealth;
//    [SerializeField] public int damage;
//    private int currentHealth;
//    private Animator animator;
//    public bool isDead = false;

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        currentHealth = maxHealth;
//    }

//    public void TakeDamage(int dame)
//    {
//        if (isDead) return;
//        currentHealth -= dame;

//        if (currentHealth <= 0)
//        {
//            isDead = true;
//            animator.SetTrigger("Die");
//            if (AudioManager.Instance != null)
//                AudioManager.Instance.PlayPlayerDeath();
//        }
//        else
//        {
//            animator.SetTrigger("Hurt");
//            if (AudioManager.Instance != null)
//                AudioManager.Instance.PlayPlayerHurt();
//        }
//    }

//    public void Attack(CharacterStat stat)
//    {
//        if (isDead) return;
//        stat.TakeDamage(damage);
//    }

//    public void RestoreHealth()
//    {
//        currentHealth = maxHealth;
//        isDead = false;
//        animator.ResetTrigger("Die");
//        animator.ResetTrigger("Hurt");
//    }
//}


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

    [Header("UI")]
    public HealthBar healthBar; // gan trong Inspector

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int dame)
    {
        if (isDead) return;

        currentHealth -= dame;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Cap nhat thanh mau 
        if (healthBar != null)
            healthBar.UpdateBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Die");
            StartCoroutine(RemoveAfterAni());

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

    private IEnumerator RemoveAfterAni()
    {
        float length = animator.GetCurrentAnimatorClipInfo(0).Length;
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }
    public void RestoreHealth()
   {
       currentHealth = maxHealth;
        isDead = false;
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Hurt");
   }
}