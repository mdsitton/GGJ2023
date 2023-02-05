using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PlayerMove))]
public class PlayerCombat : MonoBehaviour, IAttackable
{

    [SerializeField]
    private float hp = 25.0f;
    [SerializeField]
    private float attackDmg = 10f;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackArea = 0.5f;

    private PlayerMove move;
    private Rigidbody2D body;

    private LayerMask enemyLayerMask;

    public float Hp => hp;

    private void Start()
    {
        move = GetComponent<PlayerMove>();
        body = GetComponent<Rigidbody2D>();
        enemyLayerMask = LayerMask.GetMask("enemy");
    }

    private void FixedUpdate()
    {
        var movementDirection = move.MovementDirection;
        var cast = Physics2D.CircleCast(transform.position, attackArea, movementDirection, attackRange);

        if (cast.collider != null)
        {
            var attackable = cast.collider.gameObject.GetComponent<IAttackable>();
            if (attackable != null)
            {
                var dmg = 5.0f * body.velocity.magnitude;
                attackable.Attack(dmg);
            }
        }
    }

    private void Update()
    {
        if (Hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void Attack(float damage)
    {
        hp -= damage;
    }
}