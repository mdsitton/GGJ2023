using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        attackingTarget,
        goingBackToStart,
    }
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private State currentState;
    private float nextAttackTime;

    public GameObject player;
    public float enemyAttackRange =50f;
    public bool chaseWhenGoingHome;


    private void Start()
    {
        startingPosition = this.transform.position;
        roamPosition = GetRoamingPosition();
        if (!player)
        {
            player = GameObject.FindWithTag("Player");
        }
        currentState = State.Roaming;
    }

    private void Update()
    {
        switch (currentState)
        {
            default:

            case State.Roaming:
                transform.position = Vector3.MoveTowards(transform.position, roamPosition, 8f * Time.deltaTime);
                FindTarget();//While Roaming look for Player or Target
                break;

            case State.ChaseTarget:
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 50f * Time.deltaTime);
                //Attack Range
                float attackRange = 10f;
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    if (Time.time > nextAttackTime)
                    {
                       // currentState = State.attackingTarget;
                        float fireRate = .5f;
                        Debug.Log("Enemy NEAR you he can ATTACK");
                        // Animation System Maybe 
                        // currentState = State.ChaseTarget; // Reset State when Animation is Finish
                        nextAttackTime = Time.time + fireRate;//Reset for Next Attack

                    }
                }
                //Chasing Distance
                float stopChasingDistance = 80f;
                if (Vector3.Distance(transform.position, player.transform.position) >= stopChasingDistance)
                {
                    //Player Got Away
                    Debug.Log("Lucky soon of a Plant you got Away");
                    currentState = State.goingBackToStart;
                }
                break;

            case State.attackingTarget:  //Dummy state to Execute Animation Wait time in         
                break;

            case State.goingBackToStart:
                transform.position = Vector3.MoveTowards(transform.position, startingPosition, 8f * Time.deltaTime);
                if (chaseWhenGoingHome)
                    FindTarget();
                if (transform.position.x == startingPosition.x)
                {
                    Debug.Log("Enemy Return Home to Phone");
                    //Reach Start Position
                    currentState = State.Roaming;
                }
                    break;
        }

        if (transform.position.x == roamPosition.x)
        {
            roamPosition = GetRoamingPosition();//Resest Random Position for Testing when he/She arrives
        }
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 random = new Vector3(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f)).normalized;//Get Random Direction
        random.y = 0f;//floor
        return startingPosition + random * Random.Range(10f, 70f);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < enemyAttackRange)
        {
            //Player Close by
            currentState = State.ChaseTarget;
            Debug.Log("Enemy Found you Runnnn");
        }
    }
}
