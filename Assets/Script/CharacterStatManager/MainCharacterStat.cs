using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterStat : CharacterStat
{
    private Controller controller;
    private HealthBar healthBar;
    private bool isShielding;

    private bool isShield;

    protected override void Start()
    {
        base.Start();
        isShield = false;
        controller = GetComponent<Controller>();
        UpdateHealthUI();
        isShielding = false;
    }

    public override void TakeDamage(int dame)
    {
        if (isShielding)
        {
            dame = 2;
        }
        base.TakeDamage(dame);
        UpdateHealthUI();
        if (isDead)
        {          
            if (controller != null)
            {
                controller.enabled = false;
                controller.StopImmediately();
            }
            StartCoroutine(WaitForDeathAnimationAndRespawn());

            if (animator != null)
            {
                animator.ResetTrigger("Hurt");
                animator.ResetTrigger("Jump");
                animator.ResetTrigger("Fight");
                animator.ResetTrigger("Specialskill");
                animator.ResetTrigger("Shield");
                animator.ResetTrigger("Run");
                animator.SetFloat("Speed", 0f);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsGrounded", true);
            }
        }
    }

    public override void RestoreHealth()
    {
        base.RestoreHealth();
        UpdateHealthUI();
    }

    public IEnumerator WaitForDeathAnimationAndRespawn()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Die"));

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float waitTime = stateInfo.length;
        yield return new WaitForSeconds(waitTime);

        if (controller != null)
        {
            yield return controller.StartCoroutine(controller.Respawn());
        }

        animator.Play("Idle", 0, 0f);

        if (controller != null)
            controller.enabled = true;
    }

    public virtual void SetHealthBar(HealthBar hb)
    {
        healthBar = hb;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.UpdateBar(currentHealth, maxHealth);
        }
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);
        UpdateHealthUI();
    }

    public virtual void BufHeal(int heal, int increaseMaxHealth)
    {
        base.BuffHeal(heal, increaseMaxHealth);
        UpdateHealthUI();
    }

    public IEnumerator ActivateShield()
    {
        if (isShielding) yield break; 

        isShielding = true;
        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        isShielding = false;
        animator.ResetTrigger("Shield");
    }
}
