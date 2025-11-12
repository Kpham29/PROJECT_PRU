//using Cinemachine;
//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;

//namespace Script.CharactorSelection
//{
//    public class Character : MonoBehaviour
//    {
//        public GameObject[] characterPrefabs;
//        public GameObject spawnPoint;

//        public GameObject mainCamera;

//        [Header("UI References")]
//        public HealthBar healthBarPrefab;
//        public Transform canvasTransform;

//        private static GameObject persistentCharacter;
//        private static GameObject persistentCamera;

//        void Start()
//        {
//            // Destroy old persistent objects if they exist
//            if (persistentCharacter != null)
//            {
//                Destroy(persistentCharacter);
//                persistentCharacter = null;
//                Debug.Log("destroyed old persistent character");
//            }

//            if (persistentCamera != null)
//            {
//                Destroy(persistentCamera);
//                persistentCamera = null;
//                Debug.Log("destroyed old persistent camera");
//            }

//            int index = PlayerPrefs.GetInt("SelectedCharacter", 1);
//            GameObject character = Instantiate(characterPrefabs[index], spawnPoint.transform.position, Quaternion.identity);
//            persistentCharacter = character;

//            if (character != null)
//            {
//                character.tag = "Player";
//                DontDestroyOnLoad(character);
//                if (healthBarPrefab != null && canvasTransform != null)
//                {
//                    HealthBar hb = Instantiate(healthBarPrefab, canvasTransform);
//                    MainCharacterStat stat = character.GetComponent<MainCharacterStat>();
//                    if (stat != null)
//                    {
//                        stat.SetHealthBar(hb);
//                    }
//                    else
//                    {
//                        Debug.LogWarning("CharacterStat component not found on character!");
//                    }
//                }
//                else
//                {
//                    Debug.LogWarning("HealthBarPrefab or CanvasTransform not assigned!");
//                }
//            }
//            else
//            {
//                Debug.LogError("Character instantiation failed!");
//                return;
//            }

//            if (mainCamera != null)
//            {
//                persistentCamera = mainCamera;
//                DontDestroyOnLoad(mainCamera);

//                CinemachineVirtualCamera followObject = mainCamera.GetComponent<CinemachineVirtualCamera>(); 
//                // Gán camera
//                followObject.Follow = character.transform;
//                followObject.LookAt = character.transform;
//                Debug.Log("Character: Player instantiated and set as target: " + character.name);
//            }
//            else
//            {
//                Debug.LogError("mainCamera is not assigned in Inspector!");
//            }

//            Controller controller = character.GetComponent<Controller>();
//            if (controller != null)
//            {
//                controller.SetInitialSpawnPoint(spawnPoint.transform.position, Quaternion.identity);
//            }
//        }
//    }
//}


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

        [Header("Cameras")]
        public GameObject mainCamera;      // CM Main Camera
        public GameObject minimapCamera;   // ← THÊM: CM Minimap Camera

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

                // Health Bar setup
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

            // 🔥 GÁN MAIN CAMERA
            SetupMainCamera(character.transform);

            // 🔥 GÁN MINIMAP CAMERA
            SetupMinimapCamera(character.transform);

            // Controller setup
            Controller controller = character.GetComponent<Controller>();
            if (controller != null)
            {
                controller.SetInitialSpawnPoint(spawnPoint.transform.position, Quaternion.identity);
            }
        }

        void SetupMainCamera(Transform player)
        {
            if (mainCamera != null)
            {
                persistentCamera = mainCamera;
                DontDestroyOnLoad(mainCamera);

                CinemachineVirtualCamera mainCam = mainCamera.GetComponent<CinemachineVirtualCamera>();
                if (mainCam != null)
                {
                    mainCam.Follow = player;
                    mainCam.LookAt = player;
                    Debug.Log("✅ Main Camera: Theo dõi player - " + player.name);
                }
                else
                {
                    Debug.LogError("mainCamera không có CinemachineVirtualCamera!");
                }
            }
            else
            {
                Debug.LogError("mainCamera is not assigned in Inspector!");
            }
        }

        void SetupMinimapCamera(Transform player)
        {
            if (minimapCamera != null)
            {
                DontDestroyOnLoad(minimapCamera); // Giữ minimap qua scene

                CinemachineVirtualCamera miniCam = minimapCamera.GetComponent<CinemachineVirtualCamera>();
                if (miniCam != null)
                {
                    miniCam.Follow = player;
                    Debug.Log("✅ Minimap Camera: Theo dõi player - " + player.name);
                }
                else
                {
                    Debug.LogError("minimapCamera không có CinemachineVirtualCamera!");
                }
            }
            else
            {
                Debug.LogWarning("minimapCamera is not assigned in Inspector! (Minimap sẽ không hoạt động)");
            }
        }
    }
}