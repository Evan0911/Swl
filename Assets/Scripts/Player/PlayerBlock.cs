using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public LongPressGesture lgGesture;
    public PressGesture pressGesture;
    public Animator animator;

    bool isPress;
    float pressTime;


    private void Start()
    {
        lgGesture.StateChanged += longPressStateChangedHandler;
        pressGesture.StateChanged += pressStateChangedHandler;
    }

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
                }
            }
        }
    }

    void pressStateChangedHandler(object sender, GestureStateChangeEventArgs e)
    {
        isPress = true;
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
}
