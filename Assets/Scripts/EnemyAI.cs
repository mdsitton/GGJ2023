using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Start()
    {
        startingPosition = this.transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, roamPosition, 2f * Time.deltaTime);

        if (transform.position.x == roamPosition.x)
        {
            roamPosition = GetRoamingPosition();//Resest Random Position for Testing when he/She arrives
        }
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 random = new Vector3(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f)).normalized;//Get Random Direction
        return startingPosition + random * Random.Range(10f, 70f);
    }
}
