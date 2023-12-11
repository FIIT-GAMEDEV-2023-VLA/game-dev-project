using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBouldersCollisionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxCollisionVelocityMagnitude = 2f;
    [SerializeField] private float minBoulderVelocityMagnitude = 0.1f;
    private bool isReleased = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && rb.velocity.magnitude > maxCollisionVelocityMagnitude)
        {
            other.gameObject.SendMessage("CollideWithTrap");
            Debug.Log("Player collided with a fast Boulder! Oops!");
        }
    }
    private void Update()
    {
        if (isReleased && rb.velocity.magnitude <= minBoulderVelocityMagnitude)
        {
            Destroy(gameObject);
        }
    }

    public void SetReleased()
    {
        isReleased = true;
    }
}
