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

        // 🩸 Thêm 2 dòng này để gán HealthBar UI
        [Header("UI References")]
        public HealthBar healthBarPrefab;
        public Transform canvasTransform;


        void Start()
        {
            int index = PlayerPrefs.GetInt("SelectedCharacter", 1);
            GameObject character = Instantiate(characterPrefabs[index], spawnPoint.transform.position, Quaternion.identity);

            if (character != null)
            {
                character.tag = "Player";
                DontDestroyOnLoad(character);

                // 🧠 Tạo HealthBar cho nhân vật
                if (healthBarPrefab != null && canvasTransform != null)
                {
                    HealthBar hb = Instantiate(healthBarPrefab, canvasTransform);
                    CharacterStat stat = character.GetComponent<CharacterStat>();
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

                // Gán camera
                FollowObject.SetTarget(character);
                Debug.Log("Character: Player instantiated and set as target: " + character.name);
            }
            else
            {
                Debug.LogError("Character instantiation failed!");
                return;
            }

            if (mainCamera != null)
            {
                DontDestroyOnLoad(mainCamera);
                if (mainCamera.GetComponent<FollowObject>() == null)
                {
                    Debug.LogError("FollowObject component not found on mainCamera!");
                }
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
    }
}