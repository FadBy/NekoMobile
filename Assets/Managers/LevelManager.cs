using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    public int countObstacle;
    public GameObject[] prefabObstacles;
    public GameObject startLevel;
    private Transform startPoint;
    private GameObject[] obstaclesObj;
    private ObstacleGroup[] obstacles;

    private void Awake()
    {
        startPoint = transform.Find("StartPoint");
        obstaclesObj = new GameObject[countObstacle];
        obstacles = new ObstacleGroup[countObstacle];
        for (int i = 0; i < countObstacle; i++)
        {
            if (i == 0)
            {
                obstaclesObj[0] = startLevel;
                obstacles[0] = obstaclesObj[0].GetComponent<ObstacleGroup>();
            }
            obstaclesObj[i] = prefabObstacles[UnityEngine.Random.Range(0, prefabObstacles.Length)];
            obstacles[i] = obstaclesObj[i].GetComponent<ObstacleGroup>();
        }
    }

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        Vector3 currentPos = startPoint.position;
        for (int i = 0; i < countObstacle; i++)
        {
            Instantiate(obstaclesObj[i].gameObject, currentPos, Quaternion.identity);
            currentPos += new Vector3(0f, obstacles[i].length, 0f);
        }
    }

}
