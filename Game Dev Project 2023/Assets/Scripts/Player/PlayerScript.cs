//using System.Collections;
//using System.Collections.Generic;

using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float moveSpeed = 5f;
    public float jumpingPower = 12f;
    public float slideForce = 5f;

    //TODO: Find out if its better to use GetComponent in Start() instead of SerializeField
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private Light2D playerLight2D;

    private bool isInputLocked;
    private bool isFacingRight;
    private bool isAlive;
    private enum AnimationState { Idle, Running, Jumping, Falling, Sliding, CrouchingIdle, CrouchingRunning};
    
    void Start()
    {
        int idScene = SceneManager.GetActiveScene().buildIndex;  // if current scene is saved game
        if (idScene==2)
        {
            SaveManagerScript saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManagerScript>();
            Data data = saveManager.LoadMyStuffPlease();
            var x = data.positionX;
            var y = data.positionY;
            var z = data.positionZ;

            transform.position = new Vector3(x, y, z);
        }
        isInputLocked = false;
        isFacingRight = true;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (!isInputLocked && isAlive)
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && IsGrounded() && !isInputLocked) //TODO: change this to a different input and adjust conditions
        {
            float force = (isFacingRight) ? slideForce : (-1 * slideForce);
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
            anim.SetTrigger("slide");
        }

        if (isAlive)
        {
            UpdateAnimations();
        }

    }
    
    private void FixedUpdate()
    {
        if (!isInputLocked && isAlive)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision!" );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger!" );
        Die();
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    public void Die()
    {
        anim.SetTrigger("death");
        isAlive = false;
    }

    private void UpdateAnimations(){

        AnimationState animState;
        
        if (horizontal > 0f && !isInputLocked) {
            animState = AnimationState.Running;
            spriteRend.flipX = false;
            isFacingRight = true;
        }
        else if (horizontal < 0f && !isInputLocked) {
            animState = AnimationState.Running;
            spriteRend.flipX = true;
            isFacingRight = false;
        }
        else {
            animState = AnimationState.Idle;
        }
        
        if (vertical < -0.1f && IsGrounded())
        {
            animState = AnimationState.CrouchingIdle;
            if (horizontal != 0)
            {
                animState = AnimationState.CrouchingRunning;
            }
        }
        
        if (rb.velocity.y > .1f) {
            animState = AnimationState.Jumping;
        }
        else if (rb.velocity.y < -.1f) {
            animState = AnimationState.Falling;
        }
        anim.SetInteger("animState", (int)animState);
    }

    public void LockInput()
    {
        //Debug.Log("Animations have been locked!");
        isInputLocked = true;
    }

    public void UnlockInput()
    {
        //Debug.Log("Animations have been unlocked!");
        isInputLocked = false;
    }
}
