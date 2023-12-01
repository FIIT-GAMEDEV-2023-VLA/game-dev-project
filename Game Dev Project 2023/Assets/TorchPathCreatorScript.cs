using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;


#if UNITY_EDITOR
public class TorchPathCreatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CustomEditor(typeof(TorchPathCreatorScript))]
public class TorchPathCreator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add a Point"))
        {
            var creator = (TorchPathCreatorScript)target;
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif