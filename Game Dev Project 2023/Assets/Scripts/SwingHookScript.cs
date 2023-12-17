// Author: Leonard Puškáč

using System;
using UnityEngine;

public class SwingHookScript : MonoBehaviour
{
    [SerializeField] private GameObject chainPrefab;
    [SerializeField] private DistanceJoint2D distanceJoint2D;
    private GameObject player;
    private PlayerScript playerScript;
    private Rigidbody2D playerRigidBody2D;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            playerScript = player.GetComponent<PlayerScript>();
            playerRigidBody2D = player.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: set player to canAttachToHook
        AttachPlayer();
    }

    public void AttachPlayer()
    {
        distanceJoint2D.connectedBody = playerRigidBody2D;
        distanceJoint2D.connectedAnchor = player.transform.position;
    }

    public void UnattachPlayer()
    {
        
    }
}
