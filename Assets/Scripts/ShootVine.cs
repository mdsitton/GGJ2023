using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVine : MonoBehaviour
{
    public float vineSpeed = 0.000002f;
    public bool attach = false;

    public Rigidbody2D myRigidBody;

    private Vector2 heading;
    private Vector2 direction;

    public Vector3 targetPos;
    public Vector2 targetPos2D;
    public Vector2 playerPos;

    public PlayerMove player;

    [SerializeField]
    private VineRenderer vineRenderer;

    private float aliveTime = 0.0f;
    private bool isAttached = false;

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
        aliveTime += Time.deltaTime;

        if (aliveTime > 3 && !isAttached)
        {
            vineRenderer.Retract();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isAttached = false;
            vineRenderer.Retract();
        }

        if (isAttached)
        {
            var temp = myRigidBody.position-player.playerBody.position;
            temp = temp / temp.magnitude*0.1f;
            //temp += player.playerBody.velocity;
         
            player.playerBody.velocity = player.playerBody.velocity+temp;//direction * vineSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Platform")
        {
            myRigidBody.velocity = Vector2.zero;
            isAttached = true;

            print("collide");

        }


    }


}
