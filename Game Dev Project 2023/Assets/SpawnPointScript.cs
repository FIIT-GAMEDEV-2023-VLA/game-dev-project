using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.7f);
        Gizmos.color = prevColor;
    }
}
