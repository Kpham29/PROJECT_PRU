using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject currentMap;
    [SerializeField] private GameObject nextMap;
    [SerializeField] private Transform startPoint;
    [SerializeField] private bool winGame =  false;

    private void Start()
    {
        currentMap.SetActive(true);
        Transform boundaryObj = currentMap.transform.Find("Boundary");
        if (boundaryObj != null)
        {
            PolygonCollider2D boundary = boundaryObj.GetComponent<PolygonCollider2D>();
            if (boundary != null && CameraConfinerManager.Instance != null)
            {
                CameraConfinerManager.Instance.SetMapBoundary(boundary);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (winGame)
            {
                SceneManager.LoadScene("Outro");
            }
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
                // Cập nhật camera target khi chuyển map
                if (CameraConfinerManager.Instance != null)
                {
                    CameraConfinerManager.Instance.UpdateCameraTarget();
                } 
            }
            Transform nextBoundaryObj = nextMap.transform.Find("Boundary");
            if (nextBoundaryObj != null)
            {
                PolygonCollider2D newBoundary = nextBoundaryObj.GetComponent<PolygonCollider2D>();
                if (newBoundary != null && CameraConfinerManager.Instance != null)
                {
                    CameraConfinerManager.Instance.SetMapBoundary(newBoundary);
                }
            }
        }
    }
}