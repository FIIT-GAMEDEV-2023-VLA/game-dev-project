// Author: Leonard Puškáč
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpingPower = 12f;
    public float slideForce = 5f;
    public float moveSpeedModifier = 1f;
    public float groundCheckOverlapRadius = 0.3f;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;
    private Vector2 capsuleCollider2DOffset;
    private Vector2 capsuleCollider2DSize;
    
    [SerializeField] public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformsLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator animLight;
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private string torchSpawnZoneTag;
    [SerializeField] private string doorwayButtonZoneTag;

    private ResourceManagerScript resourceManagerScript;
    // Reference to the active torch spawn zone path 
    private GameObject torchSpawnZonePath;
    private GameObject doorwayButton;
    // Input Axes
    private float horizontal;
    private float vertical;
    // Bool States
    private bool isInputLocked;
    private bool isFacingRight;
    private bool isAlive;
    private bool canThrowATorch;
    private bool canOpenDoor;
    private enum AnimationState { Idle, Running, Jumping, Falling, Sliding, CrouchingIdle, CrouchingRunning};
    
    void Start()
    {
        isAlive = true;
        isInputLocked = false;
        isFacingRight = true;
        canThrowATorch = false;
        torchSpawnZonePath = null;
        canOpenDoor = false;
        doorwayButton = null;

        capsuleCollider2DOffset = capsuleCollider2D.offset;
        capsuleCollider2DSize = capsuleCollider2D.size;
        
        resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>();
        animLight.Play("PlayerLight_Flickering");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z != 0)
        {
            // LAZY BUG FIX - SUE ME!!!
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        } 
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

            if (Input.GetKeyDown(KeyCode.E) && canThrowATorch && torchSpawnZonePath && resourceManagerScript.GetTorchCount() > 0)
            {   
                TorchPathScript torchPathScript = torchSpawnZonePath.GetComponent<TorchPathScript>(); 
                torchPathScript.SpawnTorch(transform.position);
                resourceManagerScript.RemoveTorch(1);
            }

            if (Input.GetKeyDown(KeyCode.E) && canOpenDoor && doorwayButton)
            {   
                doorwayButton.GetComponent<DoorwayButtonScript>().Open();
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
        }

        if (other.gameObject.CompareTag(doorwayButtonZoneTag))
        {
            Debug.Log("Entered Button Zone");
            doorwayButton = other.gameObject;
            Debug.Log(doorwayButton);
            canOpenDoor = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag(torchSpawnZoneTag))
        {
            canThrowATorch = false;
            torchSpawnZonePath = null;
        }

        if (other.gameObject.CompareTag(doorwayButtonZoneTag))
        {
            Debug.Log("Exited Button Zone");
            canOpenDoor = false;
            doorwayButton = null;
        }
    }
    
    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckOverlapRadius, groundLayer) || 
            Physics2D.OverlapCircle(groundCheck.position, groundCheckOverlapRadius, platformsLayer))
        {
            return true;
        }
        return false;
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
        capsuleCollider2D.size = new Vector2(capsuleCollider2DSize.x, capsuleCollider2DSize.y / 2f);
        capsuleCollider2D.offset = new Vector2(capsuleCollider2DOffset.x, capsuleCollider2DOffset.y - (capsuleCollider2DSize.y / 4f));
    }

    public void SetNormalHitBox()
    {
        capsuleCollider2D.size = capsuleCollider2DSize;
        capsuleCollider2D.offset = capsuleCollider2DOffset;
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
