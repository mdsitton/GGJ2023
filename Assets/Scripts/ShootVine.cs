using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVine : MonoBehaviour
{
    public float vineSpeed = 0.000002f;
    public bool attach = false;

    public Rigidbody2D rigidBody;

    public PlayerMove player;

    [SerializeField]
    private VineRenderer vineRenderer;

    private float aliveTime = 0.0f;
    private bool isAttached = false;

    // Start is called before the first frame update
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();

        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        Vector3 heading = targetPos - transform.position;
        Vector3 direction = heading.normalized;
        rigidBody.velocity += (Vector2)direction * vineSpeed;

        transform.position = transform.position + direction * 1f;
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
            var temp = rigidBody.position - player.playerBody.position;
            temp = temp / temp.magnitude * 0.2f;

            if (player.playerBody.velocity.x > 10)
                player.playerBody.velocity = new Vector2(10, player.playerBody.velocity.y);
            if (player.playerBody.velocity.y > 10)
                player.playerBody.velocity = new Vector2(player.playerBody.velocity.x, 10);
            if (player.playerBody.velocity.x < -10)
                player.playerBody.velocity = new Vector2(-10, player.playerBody.velocity.y);
            if (player.playerBody.velocity.y < -10)
                player.playerBody.velocity = new Vector2(player.playerBody.velocity.x, -10);
            //temp += player.playerBody.velocity;

            player.playerBody.velocity = player.playerBody.velocity + temp;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Platform")
        {
            rigidBody.velocity = Vector2.zero;
            isAttached = true;
            vineRenderer.Attached();

            print("collide");

        }


    }


}
