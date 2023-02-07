using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField] float currentHealth = 3;
    public Image[] images = new Image[3];
    public PlayerCombat combat;
    private void Start()
    {
        currentHealth = 3;
        ShowCurrentHealth();
    }

    private void LateUpdate()
    {
        ShowCurrentHealth();

    }

    private void ShowCurrentHealth()
    {
        currentHealth = combat.Hp;

        if (currentHealth <= 25)
        {
            images[0].enabled = true;
            images[1].enabled = false;
            images[2].enabled = false;
        }

        if (currentHealth <= 15)
        {
            images[0].enabled = false;
            images[1].enabled = true;
            images[2].enabled = false;
        }

        if (currentHealth <= 5)
        {
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = true;

        }
    }
}