using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallingObject : MonoBehaviour
{
    public bool isEnemy;
    public GameObject gag;
    [HideInInspector]
    public Collider2D col2D;
    private Pattern parent;

    private void Awake()
    {
        parent = transform.parent.GetComponent<Pattern>();
    }

    public void CacheComponents()
    {
        col2D = GetComponent<Collider2D>();
    }

}
