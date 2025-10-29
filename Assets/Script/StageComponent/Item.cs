using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(DisplayItem());
        }
    }

    private IEnumerator DisplayItem()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}


