using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private MainCharacterStat player;

    void Start()
    {
        player = GetComponentInParent<MainCharacterStat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BeserkStats stat = collision.GetComponent<BeserkStats>();
            BossStats bossstas = collision.GetComponent<BossStats>();
            if (stat != null && player != null)
            {
                stat.TakeDamage(player.damage);
                Debug.Log("danh trung");
            }

            if (bossstas != null && player != null)
            {
                bossstas.TakeDamage(player.damage);
                Debug.Log("danh trung Boss");
            }
        }
    }
}
