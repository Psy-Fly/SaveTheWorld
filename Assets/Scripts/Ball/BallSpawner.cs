using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] public float spawnRate = 1;

    [SerializeField] public List<Ball> allBallsList;
    [SerializeField] public List<Ball> currentBallList;

    public Action<Ball> BallsCreated;
    private int ballId = 0;
    void Start()
    {
        AddNewBall(0);
        StartCoroutine(SpawnBalls());
        
    }

    public void AddNewBall(int ballId)
    {
        if (ballId < allBallsList.Count)
        {
            currentBallList.Add(allBallsList[ballId]);
        }

        
    }

    IEnumerator SpawnBalls()
    {
        while (true)
        {

            Vector3 spawnRange = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f,0.9f),1.1f,0));

            Ball ball =  Instantiate(GetRandomBall(), spawnRange, Quaternion.Euler(0, 0, 0));
            ball.Init(ballId++);
            BallsCreated.Invoke(ball);
            yield return new WaitForSeconds(1/spawnRate);
        }
    }

    private Ball GetRandomBall()
    {
        
        int chanceSum = 0, i = 0;
        Ball nextBall;
        
        for (int j = 0; j < currentBallList.Count; j++)
        {
            chanceSum += currentBallList[j].dropChance;
        }
        int randInt = Random.Range(0, chanceSum);
        for (i = 0; i < currentBallList.Count; i++)
        {
            randInt -= currentBallList[i].dropChance;
            if(randInt <= 0) break;
        }

        nextBall = currentBallList[i];

        return nextBall;
    }
    
}