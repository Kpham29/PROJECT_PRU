using UnityEngine;
using Cinemachine;

public class CameraConfinerManager : MonoBehaviour
{
    public static CameraConfinerManager Instance { get; private set; }

    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        confiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    public void SetMapBoundary(PolygonCollider2D newBoundary)
    {
        if (confiner == null) return;

        confiner.m_BoundingShape2D = newBoundary;
    }
}
