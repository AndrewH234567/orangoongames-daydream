using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour
{
    public GameObject wormSpawnerPrefab;   // drag WormSpawner prefab here
    public Transform[] spawnPoints;        // drag your 10 spawn points here

    void Start()
    {
        StartCoroutine(SpawnSpawners());
    }

    IEnumerator SpawnSpawners()
    {
        for (int i = 0; i <= spawnPoints.Length; i++)
        {
            Instantiate(wormSpawnerPrefab, spawnPoints[i].position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Debug.Log(i);
            Debug.Log(spawnPoints.Length);
            if (i == spawnPoints.Length - 1)
            {
                Debug.Log(i);
                i = 0;
            }
        }
    }
}
