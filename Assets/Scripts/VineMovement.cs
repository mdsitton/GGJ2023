using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VineState
{
    FIRE,
    ATTACH,
    RETRACT,
    PULL,
}

public delegate void DestroyCallback(VineMovement vine);

public class VineMovement : MonoBehaviour
{
    public float vineSpeed = 0.000002f;

    public Rigidbody2D rigidBody;

    public PlayerMove player;

    [SerializeField]
    private VineRenderer vineRenderer;

    public Transform playerTransform;

    private float aliveTime = 0.0f;

    // Vine current state
    public VineState State { get; set; } = VineState.FIRE;

    // Convince state properties 
    private bool isAttached => State == VineState.ATTACH;
    private bool isRetracting => State == VineState.RETRACT;
    private bool isFiring => State == VineState.FIRE;
    private bool isPulling => State == VineState.PULL;

    public DestroyCallback DestroyCallback { get; set; }


    public float retractTimer = 1f;

    private Vector3 endRetractVelocity;

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

    public void StopMovement()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime += Time.deltaTime;

        switch (State)
        {
            case VineState.FIRE:
                if (aliveTime > 3)
                {
                    State = VineState.RETRACT;
                }
                break;

            // Attach state just stays still and no active code is happening here
            // rendering code has a lot happening for this state
            case VineState.ATTACH:
                break;

            case VineState.RETRACT:
                StopMovement();
                retractTimer -= Time.deltaTime;
                transform.position = Vector3.SmoothDamp(transform.position, playerTransform.position, ref endRetractVelocity, retractTimer * 0.3f);

                var distance = Vector3.Distance(transform.position, playerTransform.position);
                if (distance < 0.5f)
                {
                    DestroyCallback?.Invoke(this);
                    Destroy(gameObject);
                }
                break;

            case VineState.PULL:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            StopMovement();
            State = VineState.ATTACH;
        }
    }


}
