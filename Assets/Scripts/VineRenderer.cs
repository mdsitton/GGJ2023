using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(VineMovement))]
public class VineRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    private Transform vineTransform;

    private Vector3[] tightenTargets;
    private Vector3[] lineSegments;
    private Vector3[] segmentVelocity;

    private VineMovement movement;

    public Transform playerTransform;

    private void Start()
    {
        movement = GetComponent<VineMovement>();
        lineRenderer = GetComponent<LineRenderer>();
        vineTransform = GetComponent<Transform>();
        tightenTargets = new Vector3[15];
        lineSegments = new Vector3[15];
        segmentVelocity = new Vector3[15];

        var body = GetComponent<Rigidbody2D>();

        // inherit segment velocities from rigidbody
        for (int i = 0; i < segmentVelocity.Length; ++i)
        {
            segmentVelocity[i] = body.velocity;
        }

        GenerateLinePoints(lineSegments, playerTransform.position, vineTransform.position);
    }

    private Vector3 RandomVector(float scale)
    {
        return new Vector3(UnityEngine.Random.Range(-scale, scale), UnityEngine.Random.Range(-scale, scale), 0.0f);
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
        GenerateLinePoints(tightenTargets, playerTransform.position, vineTransform.position);
        switch (movement.State)
        {
            case VineState.FIRE:
                lineSegments[0] = vineTransform.position;
                for (int i = 1; i < lineSegments.Length - 1; ++i)
                {
                    lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], lineSegments[i - 1], ref segmentVelocity[i], 0.3f);
                    lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], tightenTargets[i], ref segmentVelocity[i], 0.5f);
                }
                lineSegments[lineSegments.Length - 1] = playerTransform.position;
                break;
            case VineState.ATTACH:
                for (int i = 0; i < lineSegments.Length; ++i)
                {
                    lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], tightenTargets[i], ref segmentVelocity[i], 0.1f);
                }
                break;
            case VineState.RETRACT:
                lineSegments[0] = vineTransform.position;
                for (int i = 1; i < lineSegments.Length - 1; ++i)
                {
                    lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], playerTransform.position, ref segmentVelocity[i], movement.retractTimer * 0.3f);
                    lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], tightenTargets[i], ref segmentVelocity[i], 0.5f);
                }
                lineSegments[lineSegments.Length - 1] = playerTransform.position;
                break;
            case VineState.PULL:
                break;
        }

        lineRenderer.SetPositions(lineSegments);
    }
}