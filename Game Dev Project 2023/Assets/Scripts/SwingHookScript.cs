// Author: Leonard Puškáč

using System;
using UnityEngine;

public class SwingHookScript : MonoBehaviour
{
    [SerializeField] private GameObject chainPrefab;
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
        throw new NotImplementedException();
        // TODO: set player to canAttachToHook
    }

    public void AttachPlayer()
    {
        
    }

    public void UnattachPlayer()
    {
        
    }
}
