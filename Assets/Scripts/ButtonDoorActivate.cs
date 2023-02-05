using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorActivate : MonoBehaviour
{

    [SerializeField] bool playerPress;
    Vector3 resetPosition;

    public GameObject doorGameObject;

    private void Start()
    {
        resetPosition = this.transform.position;
       
    }
    private void Update()
    {
        if (playerPress)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, 0f), 2f * Time.deltaTime);
        }
        if (!playerPress)
        {
            transform.position = Vector3.MoveTowards(transform.position, resetPosition, 1f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            doorGameObject.GetComponent<Door>().SetLock();
            Debug.Log("Open Sesemy Seeds");
            playerPress = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Player go Bye Bye");
            ResetPosition();
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void ResetPosition()
    {
        playerPress = false;
    }
}
