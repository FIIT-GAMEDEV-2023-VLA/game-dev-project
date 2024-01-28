// Viktor Szabo
using System.Collections;
using UnityEngine;

public class DoorwayBottomCollisionScript : MonoBehaviour
{
    private bool closing = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && closing)
        {
            other.gameObject.SendMessage("CollideWithTrap");
            Debug.Log("Player got squished by a door!");
        }
    }

    public void SetClosingTrue()
    {
        closing = true;
    }

    public void SetClosingFalse()
    {
        closing = false;
    }
}