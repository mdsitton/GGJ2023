using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public GameObject Vine;
    public Vector3 targetPos;
    public Vector3 playerPos;
    public Rigidbody2D playerBody;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            {
                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);
            }

            targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos.z = 0;

            Instantiate(Vine, playerBody.position, Quaternion.identity);

        }

    }
}
