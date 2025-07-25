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
    public float DamageRate;
    public float Lifetime;

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add Postions to the list
        positionList = new List<Vector2>
        {
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
        Invoke(nameof(SelfDestroy), Lifetime); // theres probably a better way of doing this tbh
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        // Add Postions to the list
        positionList = new List<Vector2> 
        {
            startingPosition,
            endingPosition
        };


        lineRenderer.SetPosition(0, startingPosition);
        lineRenderer.SetPosition(1, endingPosition);
        edgeCollider.SetPoints(positionList);
    }
}
