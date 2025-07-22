using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetMeshColliderWithScale))]
public class SetMeshColliderWithScaleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Reference to the target script
        SetMeshColliderWithScale script = (SetMeshColliderWithScale)target;

        // Add a button to the Inspector
        if (GUILayout.Button("Apply Scaled Mesh"))
        {
            // Call the ApplyScaledMesh method from the runtime script
            script.ApplyScaledMesh();
        }
    }
}
