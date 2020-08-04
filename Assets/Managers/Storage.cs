using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Manager<Storage>
{
    public GameObject[] patternPrefabs;
    public Dictionary<GameObject, Pattern> patternComponents = new Dictionary<GameObject, Pattern>();

    private Pooler pooler;

    private void Start()
    {
        pooler = Pooler.Instance;
        foreach (GameObject patt in patternPrefabs)
        {
            foreach (GameObject obj in pooler.poolDict[patt])
            {
                patternComponents[obj] = obj.GetComponent<Pattern>();
            }
        }
    }
}
