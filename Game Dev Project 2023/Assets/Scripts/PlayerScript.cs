using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float moveSpeed = 5f;
    public float jumpingPower = 12f;

    //TODO: Find out if its better to use GetComponent in Start() instead of SerializeField
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private Light2D playerLight2D;

    private enum AnimationState { idle, running, jumping, falling, sliding, crouching};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void UpdateAnimations(){

        AnimationState animState;
        
        if (horizontal > 0f) {
            animState = AnimationState.running;
            spriteRend.flipX = false;
        }
        else if (horizontal < 0f) {
            animState = AnimationState.running;
            spriteRend.flipX = true;
        }
        else {
            animState = AnimationState.idle;
        }
        
        if (vertical < -0.1f && isGrounded())
        {
            animState = AnimationState.crouching;
        }
        

        if (rb.velocity.y > .1f) {
            animState = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f) {
            animState = AnimationState.falling;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded()) //TODO: change this to a different input and adjust conditions
        {
            animState = AnimationState.sliding;
        }

        anim.SetInteger("animState", (int)animState);
    }
}
