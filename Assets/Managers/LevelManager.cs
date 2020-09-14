using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    public GameObject[] prefabObstacles;
    public GameObject startLevel;
    private Transform startPoint;
    private GameObject[] obstaclesObj;
    private ObstacleGroup[] obstacles;

    private void Awake()
    {
        startPoint = transform.Find("StartPoint");
    }

    public void GenerateLevel(int countObstacle)
    {
        obstaclesObj = new GameObject[countObstacle];
        obstacles = new ObstacleGroup[countObstacle];
        for (int i = 0; i < countObstacle; i++)
        {
            if (i == 0)
                obstaclesObj[0] = startLevel;
            else
                obstaclesObj[i] = prefabObstacles[UnityEngine.Random.Range(0, prefabObstacles.Length)];
            obstacles[i] = obstaclesObj[i].GetComponent<ObstacleGroup>();
        }
        Vector3 currentPos = startPoint.position;
        for (int i = 0; i < countObstacle; i++)
        {
            Instantiate(obstaclesObj[i].gameObject, currentPos, Quaternion.identity);
            currentPos += new Vector3(0f, obstacles[i].length, 0f);
        }
    }

}
