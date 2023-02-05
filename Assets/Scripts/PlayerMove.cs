using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public GameObject Vine;
    public Vector3 targetPos;
    public Vector3 playerPos;
    public Rigidbody2D playerBody;

    public List<VineMovement> vines;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        vines = new List<VineMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos.z = 0;


            Vine.SetActive(false);
            var gameObject = Instantiate(Vine, playerBody.position, Quaternion.identity);

            var vine = gameObject.GetComponent<VineMovement>();
            vine.player = this;
            vine.playerTransform = GetComponent<Transform>();

            gameObject.GetComponent<VineRenderer>().playerTransform = vine.playerTransform;

            gameObject.SetActive(true);

            vines.Add(vine);
        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach (var vine in vines)
            {
                vine.State = VineState.RETRACT;
            }
        }
    }
}
