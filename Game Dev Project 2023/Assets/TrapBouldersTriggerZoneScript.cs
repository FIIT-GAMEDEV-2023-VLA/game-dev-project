// Author: Leonard Puškáč
using UnityEngine;


public class TrapBouldersTriggerZoneScript : MonoBehaviour
{
    
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private GameObject boulderContainer;
    private bool bouldersReleased = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !bouldersReleased)
        {
            // DISABLE THE CONTAINER
            edgeCollider2D.enabled = false;
            // SET FLAG
            bouldersReleased = true;
            
            // SET EACH BOULDER TO RELEASED
            foreach (Transform boulderTransform in boulderContainer.transform)
            {
                TrapBouldersCollisionScript boulderScript = boulderTransform.gameObject.GetComponent<TrapBouldersCollisionScript>();
                if (boulderScript)
                {
                    boulderScript.Release();
                    Debug.Log("Released boulder with script: " + boulderScript);
                }
            }
            
            Debug.Log("Releasing Boulders!");
        }
    }

    public void ResetFlag()
    {
        bouldersReleased = false;
    }
}
