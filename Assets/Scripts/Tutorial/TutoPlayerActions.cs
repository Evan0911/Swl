using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class TutoPlayerActions : MonoBehaviour
{
    public FlickGesture flickGesture;
    public Animator animator;

    public bool preAvoid;
    public bool preAttack;
    public bool preBlock;

    public TutorialManager tuto;

    private void Start()
    {
        flickGesture.StateChanged += flickStateChangedHandler;
        lgGesture.StateChanged += longPressStateChangedHandler;
        pressGesture.StateChanged += pressStateChangedHandler;
    }

    void flickStateChangedHandler(object sender, GestureStateChangeEventArgs e)
    {
        if (preAttack)
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

                    StartCoroutine(tuto.HasAttack());
                    preAttack = false;
                }
            }
        }
        else if (preAvoid)
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

                    StartCoroutine(tuto.HasAvoid());
                    preAvoid = false;
                }
            }
        }
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(PlayerStats.instance.atkCdFlat);
        PlayerStats.instance.canAttack = true;
        PlayerStats.instance.canMove = true;
    }

    public LongPressGesture lgGesture;
    public PressGesture pressGesture;

    bool isPress;
    float pressTime;


    private void Update()
    {
        if (isPress == true)
        {
            pressTime += Time.deltaTime;
            if (pressTime >= 0.2f)
            {
                if (PlayerStats.instance.canMove == true)
                {
                    PlayerStats.instance.isBlocking = true;
                    PlayerStats.instance.canMove = false;
                    animator.SetBool("block", true);

                    StartCoroutine(tuto.HasBlock());
                    preBlock = false;
                }
            }
        }
    }

    void pressStateChangedHandler(object sender, GestureStateChangeEventArgs e)
    {
        if (preBlock)
        {
            isPress = true;
        }
    }

    void longPressStateChangedHandler(object sender, GestureStateChangeEventArgs e)
    {
        if (lgGesture.State == Gesture.GestureState.Failed)
        {
            PlayerStats.instance.canMove = true;
            PlayerStats.instance.isBlocking = false;
            animator.SetBool("block", false);
            isPress = false;
            pressTime = 0;
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
