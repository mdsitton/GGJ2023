using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    
    [SerializeField] float healPointsPotionAmount = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var heal = collision.GetComponent<IAttackable>();
            if (heal != null)
            {
                heal. Attack(- healPointsPotionAmount);
            }
            this.gameObject.SetActive(false);
            Debug.Log("Player Power UP");
        }
    }

}
