using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapSceneFollow : MonoBehaviour
{
    private static MinimapSceneFollow instance;
    private Camera minimapCamera;
    public Vector3 offset = new Vector3(0, 0, -50);
    private Transform player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            minimapCamera = GetComponent<Camera>();
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Invoke(nameof(FindPlayer), 0.3f);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ✅ chuyển minimap camera sang Scene mới
        SceneManager.MoveGameObjectToScene(gameObject, scene);

        // đợi player spawn xong
        Invoke(nameof(FindPlayer), 0.5f);
    }

    void FindPlayer()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
            MoveToPlayerMap();
        }
    }

    void MoveToPlayerMap()
    {
        if (player == null) return;
        Vector3 newPos = player.position + offset;
        minimapCamera.transform.position = new Vector3(newPos.x, newPos.y, minimapCamera.transform.position.z);
    }
}
