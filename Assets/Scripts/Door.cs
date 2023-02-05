using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool locked;
    public float doorOpenSpeed = 2f;

    private void Update()
    {
        if (locked)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, doorOpenSpeed * Time.deltaTime);

        }
    }
    public void SetLock()
    {
        locked = true;
        OpenDoor();
    }

    void OpenDoor()
    {
        Debug.Log("Door was opened");
    }

    void CloseDoor()
    {

    }
}
