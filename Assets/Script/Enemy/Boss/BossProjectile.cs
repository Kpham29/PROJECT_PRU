using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<MainCharacterStat>().TakeDamage((int)damage);
        }
        rb.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = false;
        anim.Play("BossProjectileExplosion");
    }

    public void MoveProjectile(Transform playerTransform)
    {
        if (playerTransform == null)
        {
            rb.velocity = Vector2.down * speed;
            return;
        }

        Vector3 targetPos = playerTransform.position + new Vector3(0, 1f, 0);
        Vector2 direction = (targetPos - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }
}
