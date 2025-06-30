using UnityEngine;
using System.Collections.Generic;


public class LaserStats : MonoBehaviour
{
    public Vector3 startingPosition;
    public Vector3 endingPosition;
    private List<Vector2> positionList;
    public Color startingColor;
    public Color endingColor;

    public float Damage;
    public float Knockback;
    public bool ArmorPierce;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionList = new List<Vector2>
        {
            // Add Postions to the list
            startingPosition,
            endingPosition
        };

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        lineRenderer.startColor = startingColor;
        lineRenderer.endColor = endingColor;

        // Set the width
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Set the number of vertices
        lineRenderer.positionCount = 2;

        // Set the positions of the vertices
        lineRenderer.SetPosition(0, startingPosition);
        lineRenderer.SetPosition(1, endingPosition);
        edgeCollider.SetPoints(positionList);
        Invoke(nameof(selfDestroy), 0.5f);
    }
    public void selfDestroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, startingPosition);
        lineRenderer.SetPosition(1, endingPosition);
        edgeCollider.SetPoints(positionList);
    }
}
