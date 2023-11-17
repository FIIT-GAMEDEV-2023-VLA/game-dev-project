using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayScript : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //StartCoroutine (Delay());
    //}

    public IEnumerator Delay3()
    {
        yield return new WaitForSeconds(3);

    }
}
