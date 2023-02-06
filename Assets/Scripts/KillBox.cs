using System;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var kill = col.GetComponent<IAttackable>();
        if (kill != null)
        {
            kill.Attack(1000);
        }
    }
}