//Author: Leonard Puškáč
using UnityEngine;

public class PlayerDeathAnimLoseLifeScript : MonoBehaviour
{
    private ResourceManagerScript resourceManagerScript;

    private void Start()
    {
        resourceManagerScript = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>();
    }

    public void LoseLife()
    {
        resourceManagerScript.LoseLife(1);
    }
}
