using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEnemyAttack : MonoBehaviour
{
    public int autoAttackDamage;
    public int longAttackDamage;
    public Animator animator;
    public int longAttackDuration;
    bool isLongAttacking = false;

    public TutorialManager tuto;

    IEnumerator longAttack()
    {
        while (isLongAttacking)
        {
            PlayerHealth.instance.TakeDamage(longAttackDamage);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator longAttackTime()
    {
        yield return new WaitForSeconds(longAttackDuration);
        isLongAttacking = false;
        animator.SetBool("longAttack", false);
    }

    public IEnumerator PrepareAutoAttack()
    {
        animator.SetTrigger("autoAttack");
        yield return new WaitForSeconds(0.5f);
        animator.speed = 0;
    }
    public void AutoAttack()
    {
        animator.speed = 1;
    }

    public IEnumerator PrepareLongAttack()
    {
        animator.SetBool("longAttack", true);
        yield return new WaitForSeconds(0.49f);
        animator.speed = 0;
    }

    public void LongAttack()
    {
        animator.speed = 1;
        isLongAttacking = true;
        StartCoroutine(longAttack());
        StartCoroutine(longAttackTime());
    }
}
