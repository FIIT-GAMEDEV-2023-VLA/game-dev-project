// Author: Viktor Szabo
// pressure plate mechanics inspired by https://www.youtube.com/watch?v=Gx3F8SSorlg
// needed to make adjustments on the reset functionality due to the falling animation of the player object;
// it kept triggering Enter and Exit and the button was stuck in place

using System.Collections;
using UnityEngine;

public class DoorwayPressurePlateScript : MonoBehaviour
{
    private DoorwayScript doorway;
    public Vector3 originPos;
    private bool reset = false;
    private bool lockState = false;

    private void Start()
    {   
        originPos = transform.position;
        doorway = gameObject.GetComponentInParent<DoorwayScript>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !lockState)
        {   
            GetComponent<SpriteRenderer>().color = Color.red;
            doorway.Open();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.Translate(0, -0.01f, 0);
        }
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        reset = true;
        lockState = true;
    }

    private void Update()
    {
        if (reset)
        {
            if (transform.position.y < originPos.y)
            {
                transform.Translate(0, 0.001f, 0);
            }
            else
            {
                reset = false;
                lockState = false;
            }
        }
    }
}