using System;
using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class PlayerAttackOne : MonoBehaviour
{
    public FlickGesture flickGesture;
    public Animator animator;


    private void Start()
    {
        flickGesture.StateChanged += flickStateChangedHandler;
    }

    void flickStateChangedHandler(object sender, GestureStateChangeEventArgs e)
    {
        if (flickGesture.State == Gesture.GestureState.Recognized && PlayerStats.instance.canAttack == true && PlayerStats.instance.canMove == true)
        {
            if (flickGesture.ScreenFlickVector.y > flickGesture.ScreenFlickVector.x && flickGesture.ScreenFlickVector.y > 1)
            {
                PlayerStats.instance.canAttack = false;
                PlayerStats.instance.canMove = false;
                animator.SetTrigger("AttackOne");
                EnemyHealth.instance.TakeDamage(PlayerStats.instance.atkFlat);
                StartCoroutine(CooldownAttack());
            }
        }
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(PlayerStats.instance.atkCdFlat);
        PlayerStats.instance.canAttack = true;
        PlayerStats.instance.canMove = true;
    }
}
