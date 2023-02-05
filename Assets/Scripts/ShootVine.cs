using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVine : MonoBehaviour
{
    public float vineSpeed = 0.000002f;

    public Rigidbody2D myRigidBody;

    private Vector2 heading;
    private Vector2 direction;

    public Vector3 targetPos;
    public Vector2 targetPos2D;
    public Vector2 playerPos;

    public PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerPos = myRigidBody.position;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;
        targetPos2D = targetPos;
        heading = targetPos2D - playerPos;
        direction = heading / heading.magnitude;
        myRigidBody.velocity += direction * vineSpeed;
        Debug.Log($"Instance {player}");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Platform")
        {
            myRigidBody.velocity = Vector2.zero;

            print("collide");
        }
    }


}
