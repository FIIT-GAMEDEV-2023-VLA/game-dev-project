// Author: Leonard Puškáč
using UnityEngine;

#if UNITY_EDITOR
public class TrapArrowDipenserSpawnZoneScript : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = prevColor;
    }
}
#endif
