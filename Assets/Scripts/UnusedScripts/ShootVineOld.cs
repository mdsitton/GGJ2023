using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVineOld : MonoBehaviour
{


    public float vineSpeed = 0.0002f;

    public Rigidbody2D myRigidBody;

    private Vector2 heading;
    private Vector2 direction;


    public bool attached = false;
    
    public Vector3 targetPos;
    public Vector2 targetPos2D;
    public Vector2 playerPos;

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
       direction = myRigidBody.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    if(direction != myRigidBody.velocity){
        myRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        attached = true;
    }
    
    /*
    void OnCollisionEnter(Collision col){
        if(col.gameObject.name == "Vine"){
            if (col.gameObject.tag == "Platform"){
                myRigidBody.velocity = new Vector2(0,0);
                print("collide");
            }
        }
    }
    */

        if(Input.GetMouseButtonUp(0)){
            Destroy(gameObject);
        }
    }

}
