// Author: Leonard Puskac
using UnityEngine;

public class SaveMessagePassScript : MonoBehaviour
{
    private bool isContinued = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetContinuedFlag(bool flag)
    {
        isContinued = flag;
    }

    public bool IsContinued()
    {
        return isContinued;
    }
}
