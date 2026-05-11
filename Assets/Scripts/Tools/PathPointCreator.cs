using UnityEngine;
using UnityEditor;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;

[CustomEditor (typeof(Path))]
public class PathPointCreator : Editor
{
    SerializedProperty pathPointsProp;

    private void OnEnable()
    {
        pathPointsProp = serializedObject.FindProperty("pathPoints");
    }

    public override void OnInspectorGUI()
    { 
        serializedObject.Update();

        //EditorGUILayout.PropertyField(pathPointsProp);
        SerializedProperty propety = serializedObject.GetIterator();
        bool enterChildren = true;

        while(propety.NextVisible(enterChildren))
        {
            enterChildren = false;

            EditorGUILayout.PropertyField(propety);

            if (propety.name == "pathPoints")
            {
                GUILayout.Space(10);
                
                if (GUILayout.Button("Create new path point"))
                    CreateNewPathPoint();

                GUILayout.Space(10);

                if (GUILayout.Button("Reset path"))
                    ResetPath();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateNewPathPoint()
    {
        GameObject newPathPoint = new GameObject("NewPathPoint");
        Undo.RegisterCreatedObjectUndo(newPathPoint, "Create Object");

        pathPointsProp.arraySize++;
        SerializedProperty element = pathPointsProp.GetArrayElementAtIndex(pathPointsProp.arraySize - 1);
        element.objectReferenceValue = newPathPoint.GetComponent<Transform>();

        EditorUtility.SetDirty(target);

        Path path = (Path)target;
        Undo.SetTransformParent(newPathPoint.transform, path.transform, "Set Parent");

        Selection.activeObject = newPathPoint;
    }

    private void ResetPath()
    {
        Path path = (Path)target;
        Debug.Log(pathPointsProp.arraySize);

        for(int i = pathPointsProp.arraySize - 1; i >= 0 ; i--)
        {
            SerializedProperty element = pathPointsProp.GetArrayElementAtIndex(i);
            Transform t = element.objectReferenceValue as Transform;

            if(t != null)
                Undo.DestroyObjectImmediate(t.gameObject);
        }

        pathPointsProp.ClearArray();

        Selection.activeGameObject = path.gameObject;
    }
}
