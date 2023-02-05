using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject Vine;
    public Vector3 playerPos;
    public Rigidbody2D playerBody;

    public List<VineMovement> vines;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        vines = new List<VineMovement>();
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

        gameObject.GetComponent<VineRenderer>().playerTransform = vine.playerTransform;

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

        // check for any attached vines and drive the character towards them
        foreach (var vine in vines)
        {
            if (vine.State == VineState.ATTACH)
            {
                var temp = vine.rigidBody.position - playerBody.position;
                temp = temp / temp.magnitude * 0.2f;

                if (playerBody.velocity.x > 10)
                    playerBody.velocity = new Vector2(10, playerBody.velocity.y);
                if (playerBody.velocity.y > 10)
                    playerBody.velocity = new Vector2(playerBody.velocity.x, 10);
                if (playerBody.velocity.x < -10)
                    playerBody.velocity = new Vector2(-10, playerBody.velocity.y);
                if (playerBody.velocity.y < -10)
                    playerBody.velocity = new Vector2(playerBody.velocity.x, -10);

                playerBody.velocity += temp;
            }
        }
    }
}
