using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawner : MonoBehaviour
{
    [Header("Prefab & settings")]
    public GameObject wormPrefab;
    public int poolSize = 100;
    public float spawnInterval = 0.05f; // seconds between spawns
    public int maxActive = 80;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private List<GameObject> active = new List<GameObject>();
    private Coroutine spamCoroutine;

    void Awake()
    {
        // Pre-fill pool
        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(wormPrefab);
            go.SetActive(true);
            pool.Enqueue(go);
        }
    }

    public void StartSpamming()
    {
        if (spamCoroutine == null) spamCoroutine = StartCoroutine(SpamSpawn());
    }

    public void StopSpamming()
    {
        if (spamCoroutine != null)
        {
            StopCoroutine(spamCoroutine);
            spamCoroutine = null;
        }
    }

    IEnumerator SpamSpawn()
    {
        while (true)
        {
            if (active.Count < maxActive && pool.Count > 0)
            {
                var worm = pool.Dequeue();
                worm.transform.position = transform.position + Random.insideUnitSphere * 1f; // random offset
                worm.transform.rotation = Quaternion.identity;
                worm.SetActive(true);

                // If your worm has an initialization method, call it:
                // worm.GetComponent<Worm>().Init(...);

                active.Add(worm);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Call this from the worm when it should be "returned" to pool
    public void ReturnToPool(GameObject worm)
    {
        if (active.Remove(worm))
        {
            worm.SetActive(false);
            pool.Enqueue(worm);
        }
    }
}
