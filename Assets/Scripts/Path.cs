using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class Path : MonoBehaviour
{
    public static Path I;

    public List<Transform> pathPoints;

    public Action PathCreated;

    [Space]
    [SerializeField] LineRenderer lineRenderer;


    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;

    private void Awake()
    {
        if (I !=  null && I != this)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer.positionCount = pathPoints.Count;

        for (int i = 0; i < pathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, pathPoints[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawLineStrip(new List<Vector3>(pathPoints.Select(p => p.position)).ToArray(), false);
        }
    }
}
