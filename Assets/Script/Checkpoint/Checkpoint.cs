using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !activated)
        {
            activated = true;
            CheckpointManager.Instance.SetCheckpoint(transform.position, transform.rotation);
            Debug.Log("Checkpoint activated!");

            // Option: đổi màu hoặc hiệu ứng khi được kích hoạt
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = Color.green;
        }
    }
}
