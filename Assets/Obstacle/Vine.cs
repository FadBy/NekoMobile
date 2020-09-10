using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : Obstacle
{
    EdgeCollider2D col;
    LineRenderer line;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        col = GetComponent<EdgeCollider2D>();
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Vector3[] points3 = new Vector3[line.positionCount];
        Vector2[] points2 = new Vector2[line.positionCount];
        line.GetPositions(points3);
        for (int i = 0; i < points2.Length; i++)
        {
            points2[i] = points3[i] - transform.position;
        }
        col.points = points2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.GameOver();
    }
}
