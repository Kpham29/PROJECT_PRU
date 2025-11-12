using Cinemachine;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Script.CharactorSelection
{
    public class Character : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        public GameObject spawnPoint;

        public GameObject mainCamera;

        [Header("UI References")]
        public HealthBar healthBarPrefab;
        public Transform canvasTransform;

        private static GameObject persistentCharacter;
        private static GameObject persistentCamera;

        void Start()
        {
            // Destroy old persistent objects if they exist
            if (persistentCharacter != null)
            {
                Destroy(persistentCharacter);
                persistentCharacter = null;
                Debug.Log("destroyed old persistent character");
            }
            
            if (persistentCamera != null)
            {
                Destroy(persistentCamera);
                persistentCamera = null;
                Debug.Log("destroyed old persistent camera");
            }

            int index = PlayerPrefs.GetInt("SelectedCharacter", 1);
            GameObject character = Instantiate(characterPrefabs[index], spawnPoint.transform.position, Quaternion.identity);
            persistentCharacter = character;

            if (character != null)
            {
                character.tag = "Player";
                DontDestroyOnLoad(character);
                if (healthBarPrefab != null && canvasTransform != null)
                {
                    HealthBar hb = Instantiate(healthBarPrefab, canvasTransform);
                    MainCharacterStat stat = character.GetComponent<MainCharacterStat>();
                    if (stat != null)
                    {
                        stat.SetHealthBar(hb);
                    }
                    else
                    {
                        Debug.LogWarning("CharacterStat component not found on character!");
                    }
                }
                else
                {
                    Debug.LogWarning("HealthBarPrefab or CanvasTransform not assigned!");
                }
            }
            else
            {
                Debug.LogError("Character instantiation failed!");
                return;
            }

            if (mainCamera != null)
            {
                persistentCamera = mainCamera;
                DontDestroyOnLoad(mainCamera);
                
                CinemachineVirtualCamera followObject = mainCamera.GetComponent<CinemachineVirtualCamera>(); 
                // Gán camera
                followObject.Follow = character.transform;
                followObject.LookAt = character.transform;
                Debug.Log("Character: Player instantiated and set as target: " + character.name);
                
                // Notify CameraConfinerManager about the new player after a delay
                StartCoroutine(NotifyCameraManager());
            }
            else
            {
                Debug.LogError("mainCamera is not assigned in Inspector!");
            }

            Controller controller = character.GetComponent<Controller>();
            if (controller != null)
            {
                controller.SetInitialSpawnPoint(spawnPoint.transform.position, Quaternion.identity);
            }
        }

        private System.Collections.IEnumerator NotifyCameraManager()
        {
            // Wait longer for CameraConfinerManager to be created in the new scene
            yield return new WaitForSeconds(1f);
            
            int maxNotifications = 10;
            int notificationCount = 0;
            
            while (notificationCount < maxNotifications)
            {
                if (CameraConfinerManager.Instance != null)
                {
                    CameraConfinerManager.Instance.ForceReconnectToPlayer();
                    Debug.Log("Character: Notified CameraConfinerManager to reconnect (attempt " + (notificationCount + 1) + ")");
                    break;
                }
                
                notificationCount++;
                yield return new WaitForSeconds(0.3f);
                Debug.Log("Character: Waiting for CameraConfinerManager... attempt " + (notificationCount + 1));
            }
            
            if (notificationCount >= maxNotifications)
            {
                Debug.LogWarning("Character: Failed to find CameraConfinerManager after " + maxNotifications + " attempts");
            }
        }
    }
}