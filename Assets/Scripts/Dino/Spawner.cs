using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObjest
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }
    [SerializeField] SpawnableObjest[] objests;
    [SerializeField] float minSpawnRate = 1f;
    [SerializeField] float maxSpawnRate = 2f;
    void Start()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    public void Spawn()
    {
        if (GameManager.instance.startGame && !GameManager.instance.death)
        {
            float spawnChance = Random.value;
            foreach (var obj in objests)
            {
                if (spawnChance < obj.spawnChance)
                {
                    GameObject obstacle = Instantiate(obj.prefab);
                    obstacle.transform.position += new Vector3(transform.position.x * 2, transform.position.y);
                    break;
                }
                spawnChance -= obj.spawnChance;
            }
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
