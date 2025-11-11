using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isPickedUp = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPickedUp) return;
        if (collision.CompareTag("Player"))
        {
            isPickedUp = true;
            StartCoroutine(DisplayItem(collision.gameObject));
        }
    }

    private IEnumerator DisplayItem(GameObject player)
    {
        yield return new WaitForSeconds(1f);
        string cleanName = gameObject.name.Replace("(Clone)", "").Trim();
        var stat = player.GetComponent<MainCharacterStat>();
        switch (cleanName)
        {  
            case "BuffItem": 
                stat.BuffDamage(10);
                break;
            case "HealItem":
                stat.Heal(20);
                break;
            case "potion1":
                stat.Heal(40);
                break;
            case "potion2":
                stat.BuffDamage(20);
                break;
            case "potion3":
                stat.BuffHeal(30, 20);
                break;
        }
        Destroy(gameObject);
    }
}


