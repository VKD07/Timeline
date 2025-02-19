using UnityEngine;

namespace Player
{
    public class SpawnPastObj : MonoBehaviour
    {
        [SerializeField] private GameObject moveableObj;
        public bool hasSpawned;
        public GameObject spawnedObj;
        
        public void Spawn(Vector3 spawnPos)
        {
            if (!hasSpawned)
            {
                hasSpawned = true;
                spawnedObj = Instantiate(moveableObj, spawnPos, Quaternion.identity);
            }
        }
    }
}
