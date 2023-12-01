using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Transform = UnityEngine.Transform;


#if UNITY_EDITOR
public class TorchPathCreatorScript : MonoBehaviour
{
    [SerializeField] private GameObject triggerPrefab;
    [SerializeField] private GameObject torchPrefab;
    
    
    public void CreatePoint()
    {
        GameObject newPoint = Instantiate(original: triggerPrefab, parent: transform);
        Debug.Log("New Torch Path Trigger Point Created at coords: " + newPoint.transform.position);
    }
    
    public void SpawnTorch(Transform originTransform){
        
    }
    
    void OnDrawGizmos()
    {
        if (transform.childCount >0)
        {
            foreach (Transform child in transform)
            {
                if (child.GetSiblingIndex() < transform.childCount - 1)
                {
                    Transform nextChild = transform.GetChild(child.GetSiblingIndex() + 1);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(child.position, nextChild.position);
                }
            }
        }
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
            creator.CreatePoint();
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif