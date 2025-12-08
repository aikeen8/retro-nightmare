using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] obstacles;  // <-- array ng prefab
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn = 2f;
    private float spawntime;

    void Start()
    {
        spawntime = Time.time + timeBetweenSpawn;
    }

    void Update()
    {
        if (Time.time >= spawntime)
        {
            Spawn();
            spawntime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // Pili ng random prefab
        int index = Random.Range(0, obstacles.Length);
        GameObject prefabToSpawn = obstacles[index];

        Instantiate(
            prefabToSpawn,
            transform.position + new Vector3(randomX, randomY, 0),
            transform.rotation
        );
    }
}
