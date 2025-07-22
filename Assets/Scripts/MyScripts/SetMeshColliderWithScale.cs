using UnityEngine;

[ExecuteInEditMode]
public class SetMeshColliderWithScale : MonoBehaviour
{
    public Mesh customMesh; // Assign your custom mesh in the Inspector
    public Vector3 meshScale = Vector3.one; // Set the scale for the custom mesh (default is 1,1,1)

    public void ApplyScaledMesh()
    {
        if (customMesh == null)
        {
            Debug.LogWarning("No custom mesh assigned.");
            return;
        }

        // Scale the custom mesh
        Mesh scaledMesh = Instantiate(customMesh); // Create a copy to avoid modifying the original mesh
        Vector3[] vertices = scaledMesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].x *= meshScale.x;
            vertices[i].y *= meshScale.y;
            vertices[i].z *= meshScale.z;
        }

        scaledMesh.vertices = vertices;
        scaledMesh.RecalculateBounds(); // Recalculate bounds after scaling
        scaledMesh.RecalculateNormals(); // Optional: update normals if needed

        // Get the MeshCollider component attached to the GameObject
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        if (meshCollider != null)
        {
            // Assign the scaled mesh to the MeshCollider
            meshCollider.sharedMesh = scaledMesh;
            Debug.Log("Scaled custom mesh assigned to MeshCollider.");
        }
        else
        {
            Debug.LogWarning("No MeshCollider found on this GameObject.");
        }
    }
}
