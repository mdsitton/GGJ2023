using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform playerTransform;

    void Start()
    {
        // set to player position before we start gameplay
        // otherwise we will have the camera fly to where the player is
        cam.transform.position = playerTransform.position;
    }

    private Vector3 currentVelocity;

    void Update()
    {
        var distance = Vector3.Distance(cam.transform.position, playerTransform.position);
        var tmpPos = Vector3.SmoothDamp(cam.transform.position, playerTransform.position, ref currentVelocity, 0.7f / Mathf.Abs(distance));
        cam.transform.position = new Vector3(tmpPos.x, tmpPos.y, -10);
    }
}
