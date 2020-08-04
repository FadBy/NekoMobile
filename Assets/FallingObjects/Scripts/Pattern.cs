using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    public float Width { get; private set; }
    public float Height { get; private set; }

    private float speed = 3f;
    public float Speed { get; private set; }

    [HideInInspector]
    public Dictionary<GameObject,FallingObject> children = new Dictionary<GameObject, FallingObject>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children[child] = child.GetComponent<FallingObject>();
            children[child].CacheComponents();
        }
        CalculateSize();
    }

    private void OnEnable()
    {
        foreach (KeyValuePair<GameObject, FallingObject> child in children)
        {
            if (!child.Value.gameObject.activeSelf)
                child.Value.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        gameObject.transform.Translate(speed * Vector3.down * Time.deltaTime);
    }

    private void CalculateSize()
    {
        float maxWidth = 0;
        float maxHeight = 0;
        foreach (KeyValuePair<GameObject, FallingObject> child in children)
        {
            Vector3 pos = child.Value.col2D.bounds.max;
            if (pos.x > maxWidth)
                maxWidth = pos.x;
            if (pos.y > maxHeight)
                maxHeight = pos.y;
        }
        Width = maxWidth - transform.position.x;
        Height = maxHeight - transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndField")
            gameObject.SetActive(false);
    }
}
