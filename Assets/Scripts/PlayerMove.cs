using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject Vine;
    public Vector3 playerPos;
    public Rigidbody2D playerBody;

    private SpriteRenderer[] spriteRenderers;

    private List<VineMovement> vines;

    public Vector2 MovementDirection => playerBody.velocity.normalized;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        playerBody.interpolation = RigidbodyInterpolation2D.Extrapolate;
        vines = new List<VineMovement>();
    }

    private void FlipDirection(Vector2 direction)
    {
        if (direction.x <= 0)
        {
            foreach (var sprite in spriteRenderers)
            {
                sprite.flipX = true;
            }
        }
        else
        {
            foreach (var sprite in spriteRenderers)
            {
                sprite.flipX = false;
            }
        }
    }

    private void OnVineDelete(VineMovement vine)
    {
        vines.Remove(vine);
    }

    private void SpawnVine()
    {

        // Disable vine before we make an instance so that all objects are setup
        // when Start/Awake are called.
        Vine.SetActive(false);
        var gameObject = Instantiate(Vine, playerBody.position, Quaternion.identity);

        var vine = gameObject.GetComponent<VineMovement>();
        vine.player = this;
        vine.playerTransform = GetComponent<Transform>();
        vine.DestroyCallback += OnVineDelete;

        vine.playerTransform = GetComponent<Transform>();

        gameObject.GetComponent<VineRenderer>().playerTransform = vine.playerTransform;

        var vineRigidBody = gameObject.GetComponent<Rigidbody2D>();
        vineRigidBody.velocity = playerBody.velocity;


        gameObject.SetActive(true);

        vines.Add(vine);
    }

    private bool shootVine = false;
    private bool retractVines = false;

    private void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootVine = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            retractVines = true;
        }
    }

    Vector2 CalculateVineAttachPos(Vector2 vinePos)
    {
        var diff = playerBody.position - vinePos;
        return vinePos + diff.normalized;
    }

    private float rotationVel = 0;

    void FixedUpdate()
    {
        // check for any attached vines and drive the character towards them
        foreach (var vine in vines)
        {
            if (vine.State == VineState.ATTACH)
            {
                // var endPos = CalculateVineAttachPos();
                var posDiff = vine.rigidBody.position - playerBody.position;

                // set the velocity in the direction of the vine attach point
                playerBody.velocity += posDiff.normalized;

                // Clamp velocity to 10 m/s in any direction
                playerBody.velocity = Vector2.ClampMagnitude(playerBody.velocity, 10);

                var angle = Vector3.Angle(playerBody.position, vine.rigidBody.position);
                playerBody.AddTorque(angle, ForceMode2D.Force);
            }
        }

        if (!shootVine)
        {
            // playerBody.AddTorque(Vector2.down, ForceMode2D.Force);
            playerBody.MoveRotation(0.0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Reset frame state
        shootVine = false;
        retractVines = false;

        // If we add more input methods add separate input handlers for each of them
        CheckMouseInput();

        if (shootVine)
        {
            SpawnVine();
        }

        if (retractVines)
        {
            foreach (var vine in vines)
            {
                vine.State = VineState.RETRACT;
            }
        }

        FlipDirection(MovementDirection);
    }
}
