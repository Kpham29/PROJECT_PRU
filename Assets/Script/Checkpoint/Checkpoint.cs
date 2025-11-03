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
            Controller controller = collision.GetComponent<Controller>();
            if (controller != null)
            {
                controller.SetCheckpoint(transform.position, transform.rotation);
            }
            Debug.Log("Checkpoint activated!");

            var sr = GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = Color.green;
        }
    }
}