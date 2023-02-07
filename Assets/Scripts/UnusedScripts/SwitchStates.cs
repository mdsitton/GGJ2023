using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStates : MonoBehaviour
{
    public int playerState = 0;

    private void Awake()
    {
        SetState(playerState);// Example to call from Controler Switch Buttons
    }
    //Returns Current State
    public int CurrentState()
    {
        return playerState;
    }

    public void SetState(int setPlayerState)
    {
        playerState = setPlayerState;
        if (playerState >= 2)
        {
            //Reset 0 State
            playerState = 0;
        }
        SwithState();
    }

    void SwithState()
    {
        //Dual Player/ Combo of Both
        if (playerState == 0)
        {
            Debug.Log("State Both Pot and Plant");
        }

        //Single Player
        if (playerState == 1)
        {
            Debug.Log("Single Plant");
        }
    }
}
