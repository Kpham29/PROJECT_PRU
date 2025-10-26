using UnityEngine;

namespace Script.CharactorSelection
{
    public class Character : MonoBehaviour
    {
        public GameObject[] characterPrefabs;
        public Transform spawnPoint;
        
        // Start is called before the first frame update
        void Start()
        {
            int index = PlayerPrefs.GetInt("SelectedCharacter", 0);
            Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
