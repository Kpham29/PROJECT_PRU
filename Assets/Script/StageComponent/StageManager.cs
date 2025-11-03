using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject currentMap;
    [SerializeField] private GameObject nextMap;
    [SerializeField] private Transform startPoint;

    private void Start()
    {
        currentMap.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            currentMap.SetActive(false);
            nextMap.SetActive(true);
            if (startPoint != null)
            {
                collider.transform.position = startPoint.position;
                Controller controller = collider.GetComponent<Controller>();
                if (controller != null)
                {
                    controller.SetInitialSpawnPoint(startPoint.position, Quaternion.identity);
                }
                FollowObject.SetTarget(collider.gameObject); // Cập nhật target khi chuyển map
                Debug.Log("StageManager: Camera target updated to " + collider.gameObject.name);
            }
        }
    }
}