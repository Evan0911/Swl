using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using UnityEditor;

public class PlayerDodge : MonoBehaviour
{
    public FlickGesture flickGesture;
    public Animator animator;


    private void Start()
    {
        flickGesture.StateChanged += flickStateChangedHandler;
    }

    void flickStateChangedHandler(object sender, GestureStateChangeEventArgs e)
    {
        if (flickGesture.State == Gesture.GestureState.Recognized && PlayerStats.instance.canDodge == true && PlayerStats.instance.canMove == true)
        {
            if (flickGesture.ScreenFlickVector.x > flickGesture.ScreenFlickVector.y)
            {
                PlayerStats.instance.canMove = false;
                PlayerStats.instance.canDodge = false;
                PlayerHealth.instance.isInvicible = true;
                animator.SetTrigger("Dodge");
                StartCoroutine(CooldownDodge());
                StartCoroutine(DodgeTiming());
            }
        }
    }

    IEnumerator DodgeTiming()
    {
        yield return new WaitForSeconds(PlayerStats.instance.dodgeTime);
        PlayerStats.instance.canMove = true;
        PlayerHealth.instance.isInvicible = false;
    }
    IEnumerator CooldownDodge()
    {
        yield return new WaitForSeconds(PlayerStats.instance.dodgeCdFlat);
        PlayerStats.instance.canDodge = true;
    }
}
