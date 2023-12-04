//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEditor.TextCore.Text;

using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchThrowScript : MonoBehaviour
{
    [SerializeField] private Transform torchTransform;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private CameraControllerScript cameraControllerScript;
    private float maxDepth = -100f;
    // Start is called before the first frame update
    void Start()
    {
        cameraControllerScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControllerScript>();
        Debug.Log("Torch is Alive!");
        cameraControllerScript.LockTo(transform);
    }

    public void Update()
    {
        if (transform.position.y <= maxDepth)
        {
            TorchEnd();
        }
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

    public void TorchEnd()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("torch_death");
    }

    public void TorchDestruct()
    {
        cameraControllerScript.MoveToPlayer();
        Destroy(gameObject);
    }

    public void SetMaxDepth(float y)
    {
        maxDepth = y;
        Debug.Log("Torch Max Depth Set To: " + y);
    }

    public void Bounce(Vector3 targetPos)
    {
        Vector3 velocity = GetBounceVelocity(torchTransform.position, targetPos, 65f);
        rb.velocity = velocity;
        Debug.Log("Resulting Velocity: " + velocity);
    }
}
