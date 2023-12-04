using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchingBehaviourScript : StateMachineBehaviour
{
    private GameObject player;
    private PlayerScript playerScript;
    public void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerScript.SetMoveSpeedModifier(0.6f);
        playerScript.SetSmallHitBox();
    }

    //OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        playerScript.SetMoveSpeedModifier(1f);
        playerScript.SetNormalHitBox();
    }
}
