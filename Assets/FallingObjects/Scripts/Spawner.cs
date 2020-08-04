using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float startDelay;
    
    private Vector3 SpawnerLeft;
    private Vector3 SpawnerRight;
    private Camera cam;

    private Storage storage;
    private Pooler pooler;

    private void Awake()
    {
        cam = Camera.main;
        SpawnerRight = Camera.main.transform.Find("SpawnerRight").position;
        SpawnerLeft = Camera.main.transform.Find("SpawnerLeft").position;
    }

    private void Start()
    {
        pooler = Pooler.Instance;
        storage = Storage.Instance;
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            SpawnPattern();
            yield return new WaitForSeconds(3);
        }
    }

    private void SpawnPattern()
    {
        GameObject pattObj = storage.patternPrefabs[Random.Range(0, storage.patternPrefabs.Length)];
        pattObj = pooler.SpawnFromPull(pattObj.gameObject);
        pattObj.transform.position = new Vector3(Random.Range(SpawnerLeft.x, SpawnerRight.x - storage.patternComponents[pattObj].Width), SpawnerLeft.y);
    }


}
