using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VineRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private Transform vineTransform;
    public Transform playerTransform;

    private Vector3[] lineSegments;
    private Vector3[] segmentVelocity;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        vineTransform = GetComponent<Transform>();
        lineSegments = new Vector3[15];
        segmentVelocity = new Vector3[15];
    }

    private void Update()
    {
        lineSegments[0] = vineTransform.position;

        for (int i = 1; i < lineSegments.Length - 1; ++i)
        {
            lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], lineSegments[i - 1] + vineTransform.forward, ref segmentVelocity[i], 0.5f);
        }
        lineSegments[lineSegments.Length - 1] = playerTransform.position;
        lineRenderer.SetPositions(lineSegments);
    }
}