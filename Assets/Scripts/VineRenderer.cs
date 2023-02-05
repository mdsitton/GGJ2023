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

    private Vector3[] tightenTargets;
    private Vector3[] lineSegments;
    private Vector3[] segmentVelocity;

    private Vector3 endRetractVelocity;

    private bool isRetracting = false;
    private bool isAttached = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        vineTransform = GetComponent<Transform>();
        tightenTargets = new Vector3[15];
        lineSegments = new Vector3[15];
        segmentVelocity = new Vector3[15];

        GenerateLinePoints(lineSegments, playerTransform.position, vineTransform.position);
    }

    private Vector3 RandomVector(float scale)
    {
        return new Vector3(UnityEngine.Random.Range(-scale, scale), UnityEngine.Random.Range(-scale, scale), 0.0f);
    }

    public void Retract()
    {
        isRetracting = true;
        isAttached = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void Attached()
    {
        isAttached = true;
    }

    public void GenerateLinePoints(Vector3[] outputPoints, Vector3 startPos, Vector3 endPos)
    {
        var line = startPos - endPos;
        var distanceSegment = line.magnitude / tightenTargets.Length;

        outputPoints[0] = endPos;
        for (int i = 1; i < outputPoints.Length - 1; ++i)
        {
            outputPoints[i] = outputPoints[i - 1] + (line.normalized * distanceSegment) + RandomVector(0.1f);
        }
        outputPoints[tightenTargets.Length - 1] = startPos;
    }


    private void Update()
    {
        if (isRetracting)
        {
            vineTransform.position = Vector3.SmoothDamp(vineTransform.position, playerTransform.position, ref endRetractVelocity, 0.3f);

            lineSegments[0] = vineTransform.position;
            for (int i = 1; i < lineSegments.Length - 1; ++i)
            {
                lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], playerTransform.position, ref segmentVelocity[i], 0.3f);
            }
            lineSegments[lineSegments.Length - 1] = playerTransform.position;

            var distance = Vector3.Distance(vineTransform.position, playerTransform.position);
            if (distance < 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else if (isAttached)
        {
            GenerateLinePoints(tightenTargets, playerTransform.position, vineTransform.position);
            for (int i = 0; i < lineSegments.Length; ++i)
            {
                lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], tightenTargets[i], ref segmentVelocity[i], 0.1f);
            }
        }
        else
        {
            lineSegments[0] = vineTransform.position;
            for (int i = 1; i < lineSegments.Length - 1; ++i)
            {
                lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], lineSegments[i - 1], ref segmentVelocity[i], 0.3f);
            }
            lineSegments[lineSegments.Length - 1] = playerTransform.position;
        }

        lineRenderer.SetPositions(lineSegments);
    }
}