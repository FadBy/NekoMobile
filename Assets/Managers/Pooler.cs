using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pooler : Manager<Pooler>
{
    public List<Pool> pools;

    public Dictionary<GameObject, Queue<GameObject>> poolDict;

    [Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    private void Awake()
    {
        poolDict = new Dictionary<GameObject, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDict.Add(pool.prefab, objectPool);
        }
    }

    public GameObject SpawnFromPull(GameObject tag, Transform parent=null, bool active=true)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag.name + " doesn't exist.");
            return null;
        }

        GameObject obj = poolDict[tag].Dequeue();
        if (active)
            obj.SetActive(true);
        if (parent != null)
            obj.transform.SetParent(parent);

        poolDict[tag].Enqueue(obj);

        return obj;
    }

    public GameObject SpawnFromPull(GameObject tag, Vector3 position, Transform parent=null, bool active=true)
    {
        GameObject obj = SpawnFromPull(tag, parent, active);
        obj.transform.position = position;
        return obj;
    }
}
