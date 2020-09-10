using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    public int countObstacle;
    private Transform startPoint;
    public GameObject[] prefabObstacles;
    private ObstacleGroup[] obstacles;

    private void Awake()
    {
        startPoint = transform.Find("StartPoint");
        obstacles = new ObstacleGroup[countObstacle];
        for (int i = 0; i < countObstacle; i++)
        {
            obstacles[i] = prefabObstacles[UnityEngine.Random.Range(0, prefabObstacles.Length)].GetComponent<ObstacleGroup>();
        }
    }

    private void Start()
    {
        Vector3 currentPos = startPoint.position;
    }

    private void GenerateLevel(int level)
    {

    }

}
