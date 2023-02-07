using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVineAI : MonoBehaviour
{
    enum State
    {
        idle,
        rootGrow
    }

    State currentState;
    [SerializeField]bool plantedAllReady;

    private void Start()
    {
        ///currentState = State.idle;
    }

    private void Update()
    {
        switch (currentState)
        {
            default:
            case State.idle:
                //Animate idle
                Debug.Log("Vine Idles");
                break;
            case State.rootGrow:
                Debug.Log("Roots Grow");
                break;
        }

        CheckIfActivated();
    }

    public void RootsGrow()
    {
        currentState = State.rootGrow;
    }

    void CheckIfActivated()
    {
        if (plantedAllReady)
        {
            RootsGrow();
        }
        else
        {
            currentState = State.idle;
        }
    }
}
