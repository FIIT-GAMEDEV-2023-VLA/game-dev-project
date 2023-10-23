using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerIsAlive = true;
    public Rigidbody playerRigidBody;

    public float jumpStrength;
    public float moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && playerIsAlive)
        {
            
            playerRigidBody.velocity = Vector3.up * jumpStrength;
            Debug.Log(playerRigidBody.velocity.ToString());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && playerIsAlive)
        {
            playerRigidBody.velocity = Vector3.right * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && playerIsAlive)
        {
            playerRigidBody.velocity = Vector3.left * moveSpeed;
        }
        
    }
}
