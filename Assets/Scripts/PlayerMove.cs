using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public GameObject Vine;
    public Vector3 targetPos;
    public Vector3 playerPos;
    public Rigidbody2D playerBody;

    public List<ShootVine> vineList;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        vineList = new List<ShootVine>();
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
            var vine = gameObject.GetComponent<ShootVine>();
            vineList.Add(vine);
            var vineRenderer = gameObject.GetComponent<VineRenderer>();
            vine.player = this;
            vineRenderer.playerTransform = GetComponent<Transform>();
            gameObject.SetActive(true);
        }

    }
}
