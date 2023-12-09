// Author: Leonard Puškáč
using UnityEngine;

public class PlayerSlidingBehaviourScript : StateMachineBehaviour
{
    private GameObject player;
    private PlayerScript playerScript;
    public void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerScript.SetSmallHitBox();
        playerScript.LockInput();
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerScript.SetNormalHitBox();
        playerScript.UnlockInput();
    }
    
}
