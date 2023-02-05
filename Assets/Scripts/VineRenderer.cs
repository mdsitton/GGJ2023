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

    private Vector3 endRetractVelocity;

    private bool isRetracting = false;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        vineTransform = GetComponent<Transform>();
        lineSegments = new Vector3[15];
        segmentVelocity = new Vector3[15];

        for (int i = 1; i < lineSegments.Length; ++i)
        {
            lineSegments[i] = playerTransform.position + RandomVector(1);
        }
    }

    private Vector3 RandomVector(int scale)
    {
        return new Vector3(UnityEngine.Random.Range(-scale, scale), UnityEngine.Random.Range(-scale, scale), 0.0f);
    }

    public void Retract()
    {
        isRetracting = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void Update()
    {

        if (isRetracting)
        {
            for (int i = 0; i < lineSegments.Length; ++i)
            {
                lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], playerTransform.position, ref segmentVelocity[i], 0.5f);
            }
            vineTransform.position = Vector3.SmoothDamp(vineTransform.position, playerTransform.position, ref endRetractVelocity, 0.5f);

            var distance = Vector3.Distance(vineTransform.position, playerTransform.position);
            if (distance < 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            lineSegments[0] = vineTransform.position;
            for (int i = 1; i < lineSegments.Length - 1; ++i)
            {
                lineSegments[i] = Vector3.SmoothDamp(lineSegments[i], lineSegments[i - 1], ref segmentVelocity[i], 0.5f);
            }
            lineSegments[lineSegments.Length - 1] = playerTransform.position;
        }

        lineRenderer.SetPositions(lineSegments);
    }
}