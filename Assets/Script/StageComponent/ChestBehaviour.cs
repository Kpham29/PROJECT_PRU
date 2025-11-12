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
        yield return null;
        float clipLen = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(clipLen + 2f);
        Destroy(gameObject);
    }

    public void DropItem()
    {
        int idx = Random.Range(0, 100);
        Debug.Log(idx);
        if (idx < 10)
        {
            idx = 4;
        } else if (idx < 50)
        {
            idx = Random.Range(2, 4);
        } else if (idx < 100)
        {
            idx =  Random.Range(0, 2);
        }
        Debug.Log(idx);
        Vector3 spawnPos = dropPoint.position + Vector3.up * 0.05f;
        GameObject go = Instantiate(items[idx], spawnPos, Quaternion.identity);

        Collider2D col = go.GetComponent<Collider2D>();
        if (!col) col = go.AddComponent<CircleCollider2D>();
        col.isTrigger = true;

        // StartCoroutine(FakeDrop(go));
        go.AddComponent<DropItem>();
    }
}
