using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Stats de l'ennemi
    public int cooldown;
    public int autoAttackDamage;
    public int longAttackDamage;
    private bool canAttack = false;
    public Animator animator;
    public int longAttackDuration;
    bool isLongAttacking = false;


    private void Start()
    {
        //On d�marre le cooldown au d�but du combat
        StartCoroutine(CooldownAttack());
    }

    private void Update()
    {
        //Lorsque l'ennemi peut attaquer, choisis al�atoirement parmis ses 2 attaques
        if (canAttack)
        {
            int rnd = Random.Range(0, 2);
            Debug.Log(rnd + " " + Random.value);
            if (rnd == 0)
            {
                StartCoroutine(autoAttack());
            }
            else
            {
                StartCoroutine(longAttackAnimation());
            }
                
        }
    }

    IEnumerator autoAttack()
    {
        //Lance l'animation de pr�paration de l'attaque, emp�che l'ennemi d'attaquer et fait l'attaque apr�s 0.5 seconde avant de lancer le cooldown
        animator.SetTrigger("autoAttack");
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.instance.TakeDamage(autoAttackDamage);
        StartCoroutine(CooldownAttack());
    }

    IEnumerator CooldownAttack()
    {
        //Permet � l'ennemi d'attaquer apr�s le temps de cooldown
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    IEnumerator longAttackAnimation()
    {
        //Lance l'animation de pr�paration, emp�che l'ennemi d'attaquer puis lance l'attaque et le timer pour l'arr�ter
        animator.SetBool("longAttack", true);
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        isLongAttacking = true;
        StartCoroutine(longAttack());
        StartCoroutine(longAttackTime());
    }

    IEnumerator longAttack()
    {
        //Fais des d�g�ts continue tant que l'attaque dure
        while (isLongAttacking)
        {
            PlayerHealth.instance.TakeDamage(longAttackDamage);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator longAttackTime()
    {
        //d�sactive l'attaque apr�s le temps de celle ci �coul�
        yield return new WaitForSeconds(longAttackDuration);
        isLongAttacking = false;
        animator.SetBool("longAttack", false);
        StartCoroutine(CooldownAttack());
    }
}
