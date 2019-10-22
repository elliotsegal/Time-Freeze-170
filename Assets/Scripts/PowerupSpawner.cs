using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public Powerup prefab;
    public GameObject[] spawnPoints;

    private Powerup spawnedPowerup;

    private void Update()
    {
        if (spawnedPowerup == null)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        spawnedPowerup = Instantiate(prefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
