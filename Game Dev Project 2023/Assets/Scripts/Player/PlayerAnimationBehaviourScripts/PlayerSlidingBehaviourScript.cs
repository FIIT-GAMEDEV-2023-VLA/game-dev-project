//using System.Collections;
//using System.Collections.Generic;

using System;
using UnityEngine;

public class PlayerSlidingBehaviourScript : StateMachineBehaviour
{
    
    private GameObject player;
    private PlayerScript playerScript;
    private BoxCollider2D playerBoxCollider2D;

    private float boxColliderEnterSizeY;
    private float boxColliderEnterOffsetY;
    public void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerBoxCollider2D = player.GetComponent<BoxCollider2D>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        boxColliderEnterSizeY = playerBoxCollider2D.size.y;
        boxColliderEnterOffsetY = playerBoxCollider2D.offset.y;

        float newSizeY = playerBoxCollider2D.size.y / 2;

        playerBoxCollider2D.size = new Vector2(playerBoxCollider2D.size.x, newSizeY);
        playerBoxCollider2D.offset = new Vector2(playerBoxCollider2D.offset.x, playerBoxCollider2D.offset.y - (newSizeY / 2));
        playerScript.LockInput();
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerBoxCollider2D.size = new Vector2(playerBoxCollider2D.size.x, boxColliderEnterSizeY);
        playerBoxCollider2D.offset = new Vector2(playerBoxCollider2D.offset.x, boxColliderEnterOffsetY);
        playerScript.UnlockInput();
    }
    
}
