using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAttackable
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

    [SerializeField] private float walkingSpeed = 1;
    [SerializeField] private float runningSpeed = 5;

    public GameObject player;
    public float enemyAttackRange = 15f;
    public bool chaseWhenGoingHome;

    [SerializeField]
    private float hp;

    public float Hp => hp;

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
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        switch (currentState)
        {
            default:

            case State.Roaming:
                transform.position = Vector3.MoveTowards(transform.position, roamPosition, walkingSpeed * Time.deltaTime);
                FindTarget();//While Roaming look for Player or Target
                break;

            case State.ChaseTarget:
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, runningSpeed * Time.deltaTime);
                //Attack Range
                float attackRange = 2f;
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    if (Time.time > nextAttackTime)
                    {
                        // currentState = State.attackingTarget;
                        float fireRate = .5f;
                        Debug.Log("Enemy NEAR you he can ATTACK");
                        player.GetComponent<IAttackable>().Attack(3.0f);

                        // Animation System Maybe 
                        // currentState = State.ChaseTarget; // Reset State when Animation is Finish
                        nextAttackTime = Time.time + fireRate;//Reset for Next Attack
                    }
                }
                //Chasing Distance
                float stopChasingDistance = enemyAttackRange;
                if (Vector3.Distance(transform.position, player.transform.position) > stopChasingDistance)
                {
                    //Player Got Away
                    Debug.Log("Lucky soon of a Plant you got Away");
                    currentState = State.goingBackToStart;
                }
                break;

            case State.attackingTarget:  //Dummy state to Execute Animation Wait time in         
                break;

            case State.goingBackToStart:
                transform.position = Vector3.MoveTowards(transform.position, startingPosition, walkingSpeed * Time.deltaTime);
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
            roamPosition = GetRoamingPosition(); // Reset Random Position for Testing when he/She arrives
        }
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 random = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;//Get Random Direction
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

    public void Attack(float damage)
    {
        hp -= damage;
    }
}
