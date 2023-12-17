// Author: Leonard Puškáč
using UnityEngine;

public class TrapArrowScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 6f; 
    private float directionX = -1f;
    
    // Update is called once per frame
    void Update()
    {
        if (directionX > 0f)
        {
            spriteRenderer.flipX = true;
        }
        transform.position += new Vector3( directionX * moveSpeed * Time.deltaTime, 0, 0 );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("CollideWithTrap");
        }
        Destroy(gameObject);
    }
}
