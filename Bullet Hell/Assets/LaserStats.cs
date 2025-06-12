using UnityEngine;

public class LaserStats : MonoBehaviour
{
    public Vector3 startingPosition;
    public Vector3 endingPosition;
    public Color startingColor;
    public Color endingColor;

    public float Damage;
    public float Knockback;
    public bool ArmorPierce;

    private LineRenderer lineRenderer;
    private MeshCollider meshCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        meshCollider = gameObject.AddComponent<MeshCollider>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        lineRenderer.startColor = startingColor;
        lineRenderer.endColor = endingColor;

        // Set the width
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        // Set the number of vertices
        lineRenderer.positionCount = 2;

        // Set the positions of the vertices
        lineRenderer.SetPosition(0, startingPosition);
        lineRenderer.SetPosition(1, endingPosition);

        Mesh mesh = new();
        lineRenderer.BakeMesh(mesh, Camera.main, true);
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
