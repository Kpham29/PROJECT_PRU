using UnityEngine;

namespace Script.CharactorSelection
{
    public class Character : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        public GameObject spawnPoint;
        public GameObject camera;
        private FollowObject followObject;
        
        // Start is called before the first frame update
        void Start()
        {
            int index = PlayerPrefs.GetInt("SelectedCharacter", 0);
            GameObject character = Instantiate(characterPrefabs[index], spawnPoint.transform.position, Quaternion.identity);
            character.SetActive(true);
            if (camera != null)
            {
                followObject = camera.GetComponent<FollowObject>();
                followObject.target = character;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
