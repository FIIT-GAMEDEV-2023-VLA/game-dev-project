// Author: Leonard Puškáč
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpingPower = 12f;
    public float slideForce = 5f;
    public float moveSpeedModifier = 1f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private Vector2 boxCollider2DOffset;
    private Vector2 boxCollider2DSize;
    
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator animLight;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private string torchSpawnZoneTag;

    // Reference to the active torch spawn zone path 
    private GameObject torchSpawnZonePath;
    // Input Axes
    private float horizontal;
    private float vertical;
    // Bool States
    private bool isInputLocked;
    private bool isFacingRight;
    private bool isAlive;
    private bool canThrowATorch;
    private enum AnimationState { Idle, Running, Jumping, Falling, Sliding, CrouchingIdle, CrouchingRunning};
    
    void Start()
    {
        // TODO: replace the save manager spawn functionality - do it in the spawn manager script 
        isAlive = true;
        isInputLocked = false;
        isFacingRight = true;
        canThrowATorch = false;
        torchSpawnZonePath = null;

        boxCollider2DOffset = boxCollider2D.offset;
        boxCollider2DSize = boxCollider2D.size;
        animLight.Play("PlayerLight_Flickering");
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

            if (Input.GetKeyDown(KeyCode.LeftShift) &&
                IsGrounded()) //TODO: change this to a different input and adjust conditions
            {
                float force = (isFacingRight) ? slideForce : (-1 * slideForce);
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
                anim.SetTrigger("slide");
            }

            if (Input.GetKeyDown(KeyCode.E) && canThrowATorch && torchSpawnZonePath)
            {   
                TorchPathScript torchPathScript = torchSpawnZonePath.GetComponent<TorchPathScript>(); 
                torchPathScript.SpawnTorch(transform.position);
            }
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
            rb.velocity = new Vector2(horizontal * (moveSpeed * moveSpeedModifier), rb.velocity.y);
        }
    }

    public void CollideWithTrap()
    {
        if (isAlive)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag(torchSpawnZoneTag))
        {
            torchSpawnZonePath = other.gameObject.transform.parent.gameObject;
            canThrowATorch = true;
            Debug.Log("Player Entered a Torch Spawn Zone: "  + torchSpawnZonePath);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag(torchSpawnZoneTag))
        {
            Debug.Log("Player Exited a Torch Spawn Zone" );
            canThrowATorch = false;
            torchSpawnZonePath = null;
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }
    
    public void Spawn(Vector3 spawnPointPosition)
    {   
        isAlive = true;
        UnlockInput();
        animLight.Play("PlayerLight_Flickering");
        anim.Play("Player_Idle");
        transform.position = spawnPointPosition;
    }
    
    public void Die()
    {
        if (isAlive)
        {
            rb.velocity = Vector2.zero;
            anim.SetTrigger("death");
            animLight.SetTrigger("death");
            isAlive = false;
        }
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

    public void SetMoveSpeedModifier(float modifier)
    {
        moveSpeedModifier = modifier;
    }

    public void SetSmallHitBox()
    {
        boxCollider2D.size = new Vector2(boxCollider2DSize.x, boxCollider2DSize.y / 2f);
        boxCollider2D.offset = new Vector2(boxCollider2DOffset.x, boxCollider2DOffset.y - (boxCollider2DSize.y / 4f));
    }

    public void SetNormalHitBox()
    {
        boxCollider2D.size = boxCollider2DSize;
        boxCollider2D.offset = boxCollider2DOffset;
    }

    public void LockInput()
    {
        isInputLocked = true;
    }

    public void UnlockInput()
    {
        isInputLocked = false;
    }
}
