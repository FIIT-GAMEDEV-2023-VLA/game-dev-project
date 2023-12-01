using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    [SerializeField] private Transform playerTransform; 
    void Update()
    {
        Vector3 playerTransformPosition = playerTransform.position;
        transform.position = new Vector3(playerTransformPosition.x, playerTransformPosition.y, playerTransformPosition.z);
    }
}
