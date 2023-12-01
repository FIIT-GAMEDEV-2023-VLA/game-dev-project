using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TorchPathTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [ExecuteInEditMode]
    void OnDrawGizmosSelected()
    {
        this.transform.parent.SendMessage("OnDrawGizmosSelected");
        
    }
}
