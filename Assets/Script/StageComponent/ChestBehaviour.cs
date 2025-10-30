using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    private Animator animator;
    public GameObject[] items;
    public Transform dropPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("Open");
            StartCoroutine(OpenChest());
        }
    }

    private IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(gameObject);
    }

    public void DropItem()
    {
        int randomIndex = Random.Range(0, items.Length);
        Instantiate(items[randomIndex], dropPoint.position, Quaternion.identity);
    }
}
