// Author: Leonard Puškáč

using System.Collections;
using UnityEngine;

public class TrapBouldersCollisionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float minCollisionVelocity = 2f;
    [SerializeField] private float timeToLive = 20f;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && rb.velocity.magnitude > minCollisionVelocity)
        {
            other.gameObject.SendMessage("CollideWithTrap");
            Debug.Log("Player collided with a fast Boulder! Oops!");
        }
    }
    
    public void Release()
    {
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
