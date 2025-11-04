using UnityEngine;

namespace Script.CharactorSelection
{
    public class Character : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        public GameObject spawnPoint;
        public GameObject mainCamera; 

        void Start()
        {
            int index = PlayerPrefs.GetInt("SelectedCharacter", 1);
            GameObject character = Instantiate(characterPrefabs[index], spawnPoint.transform.position, Quaternion.identity);
            if (character != null)
            {
                character.tag = "Player";
                DontDestroyOnLoad(character); // Giữ player qua các scene
                FollowObject.SetTarget(character); // Gán target qua code
                Debug.Log("Character: Player instantiated and set as target: " + character.name);
            }
            else
            {
                Debug.LogError("Character instantiation failed!");
                return;
            }

            if (mainCamera != null)
            {
                DontDestroyOnLoad(mainCamera); // Giữ camera qua các scene
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