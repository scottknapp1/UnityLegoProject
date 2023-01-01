using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int dyingState;
    public int deadBool;
    public int walkState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int backwardsBool;
    public int Forward;
    public int strafeRightBool;
    public int strafeLeftBool;
    public int clawOpen;
    public int jump;

    private void Awake()
    {
        dyingState = Animator.StringToHash("Base Layer.Dying");
        deadBool = Animator.StringToHash("Dead");
        walkState = Animator.StringToHash("Walk");
        shoutState = Animator.StringToHash("Shouting.Shout");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        backwardsBool = Animator.StringToHash("Backwards");
        strafeLeftBool = Animator.StringToHash("Left");
        strafeRightBool = Animator.StringToHash("Right");
        Forward = Animator.StringToHash("Forward");
        clawOpen = Animator.StringToHash("openClaw");
        jump = Animator.StringToHash("jumping");
    }
}
