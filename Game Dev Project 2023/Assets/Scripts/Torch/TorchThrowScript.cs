//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.TextCore.Text;
using UnityEngine;

public class TorchThrowScript : MonoBehaviour
{
    [SerializeField] private Transform torchTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask triggerLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Torch is Alive!");
    }

    private Vector3 GetBounceVelocity(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;   
        
        float h = direction.y;                                           
        direction.y = 0;                                               
        float distance = direction.magnitude;
        Debug.Log("Direction Magnitude: " + distance);
        float a = angle * Mathf.Deg2Rad;                           
        direction.y = distance * Mathf.Tan(a);                            
        distance += h/Mathf.Tan(a); 
        
        Debug.Log("Direction: " + direction);
        Debug.Log("Distance: " + distance);
        Debug.Log("Angle In Radians: " + a);
        
        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics2D.gravity.magnitude / Mathf.Sin(2*a));
        return velocity * direction.normalized; 
    }

    public void Bounce(Vector3 targetPos)
    {
        Vector3 velocity = GetBounceVelocity(torchTransform.position, targetPos, 65f);
        rb.velocity = velocity;
        Debug.Log("Resulting Velocity: " + velocity);
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{   
    //    Debug.Log("Torch Trigger!");
    //    Vector3 velocity = GetBounceVelocity(torchTransform.position, new Vector3(-21.39f, 12.57f, 0f), 45f);
    //    rb.velocity = velocity;
    //}

    // Update is called once per frame
    //void Update()
    //{
    //}
}
