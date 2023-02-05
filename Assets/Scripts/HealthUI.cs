using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField] int currentHealth = 3;
    public Image[] images = new Image[3];

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

        if (currentHealth == 3)
        {
            images[0].enabled = true;
            images[1].enabled = false;
            images[2].enabled = false;
        }

        if (currentHealth == 2)
            {
            images[0].enabled = false;
            images[1].enabled = true;
            images[2].enabled = false;
        }

        if (currentHealth == 1)
        {
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = true;

        }
    }
}