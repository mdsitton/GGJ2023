using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivatesVines : MonoBehaviour
{
    [SerializeField]bool playerPress;
    Vector3 resetPosition;

    public GameObject vineToGrow;

    private void Start()
    {
        resetPosition = this.transform.position;
       // vineToGrow = 
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
            Debug.Log("Button Pressto");
            playerPress = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Player go Bye Bye");
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        playerPress = false;
    }
}
