// Author: Leonard Puškáč
using UnityEditor;
using UnityEngine;
using Transform = UnityEngine.Transform;


#if UNITY_EDITOR
public class TorchPathCreatorScript : MonoBehaviour
{
    [SerializeField] private GameObject torchPathTriggerPrefab;
    [SerializeField] private GameObject pathContainer;
    
    public void CreatePoint()
    {
        Vector3 newPos;
        
        if (pathContainer.transform.childCount > 0)
        {
            //Get position of last child
            Vector3 lastChildPos = pathContainer.transform.GetChild(pathContainer.transform.childCount - 1).position;
            newPos = new Vector3(lastChildPos.x, lastChildPos.y - 1, lastChildPos.z);
        }
        else
        {
            newPos = transform.position;
        }
        
        GameObject newPoint = Instantiate(original: torchPathTriggerPrefab, parent: pathContainer.transform);
        newPoint.transform.position = newPos;
        Debug.Log("New Torch Path Trigger Point Created at coords: " + newPos);
    }
    
    void OnDrawGizmos()
    {
        if (transform.hasChanged)
        {
            if (pathContainer.transform.childCount > 0)
            {
                foreach (Transform child in pathContainer.transform)
                {
                    if (child.GetSiblingIndex() < pathContainer.transform.childCount - 1)
                    {
                        Transform nextChild = pathContainer.transform.GetChild(child.GetSiblingIndex() + 1);
                        Color prevColor = Gizmos.color;
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(child.position, nextChild.position);
                        Gizmos.color = prevColor;
                    }
                }
            }
            transform.hasChanged = false;
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