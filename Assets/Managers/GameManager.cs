using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    [System.Serializable]
    public struct RandomCount
    {
        public int min;
        public int max;

        public RandomCount(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int Random()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
    public RandomCount obstacleCount;
    public GameObject playerObj;
    private HeartController player;
    private LevelManager levelManager;

    private void Start()
    {
        player = Instantiate(playerObj).GetComponent<HeartController>();
        levelManager = LevelManager.Instance;
        //Instantiate(background);
        LoadLevel();
    }

    private void LoadLevel()
    {
        levelManager.GenerateLevel(obstacleCount.Random());
        
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LoadLevel();
    }
}
