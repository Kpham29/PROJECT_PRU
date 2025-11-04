using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterStat : CharacterStat
{
    private Controller controller;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<Controller>();
    }



    public override void TakeDamage(int dame)
    {
        base.TakeDamage(dame);

        if (isDead)
        {
            if (controller != null)
            {
                controller.enabled = false;
                controller.StopImmediately();
            }
            StartCoroutine(WaitForDeathAnimationAndRespawn());
        }
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
}
