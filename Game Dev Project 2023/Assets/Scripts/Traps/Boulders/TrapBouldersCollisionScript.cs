// Author: Leonard Puškáč

using System.Collections;
using UnityEngine;

public class TrapBouldersCollisionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float minCollisionVelocity = 1f;
    private float timeToLive = 15;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && rb.velocity.magnitude > minCollisionVelocity)
        {
            other.gameObject.SendMessage("CollideWithTrap");
            Debug.Log("Player collided with a fast Boulder! Oops!");
        }
    }
    
    public void Release(float bTimeToLive, float bMinCollisionVelocity)
    {
        timeToLive = bTimeToLive;
        minCollisionVelocity = bMinCollisionVelocity;
        StartCoroutine(DeathTimer());
    }
    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
