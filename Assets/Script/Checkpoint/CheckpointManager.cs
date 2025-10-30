using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }
    private Vector3 respawnPosition;
    private Quaternion respawnRotation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCheckpoint(Vector3 position, Quaternion rotation)
    {
        respawnPosition = position;
        respawnRotation = rotation;
        Debug.Log($"Checkpoint saved at {position}");
    }

    public void Respawn(GameObject player)
    {
        if (player == null) return;

        // Đặt lại vị trí
        player.transform.position = respawnPosition;
        player.transform.rotation = respawnRotation;

        // Reset Rigidbody
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        // Reset trạng thái chết
        var stat = player.GetComponent<CharacterStat>();
        if (stat != null)
        {
            stat.isDead = false;
            // Reset lại máu (nếu muốn)
            var maxHp = stat.maxHealth;
            var field = typeof(CharacterStat).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
                field.SetValue(stat, maxHp);
        }

        Debug.Log("Player respawned at checkpoint.");
    }
}
