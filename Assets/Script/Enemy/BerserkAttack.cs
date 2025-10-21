using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkAttack : MonoBehaviour
{
    [SerializeField] protected EnemyStateMachine enemyStateMachine;
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
         
        //collision.GetComponent<PlayerStats>().DamagePlayer(damage);
    }
}
