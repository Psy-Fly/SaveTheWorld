using System;
using System.Collections;
using System.Collections.Generic;
using Bosses;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour, IBallEventHandler
{
    [SerializeField] public UIManager uiManager;
    [SerializeField] private BallSpawner ballSpawner;

    [SerializeField] private int speedUpRate;
    [SerializeField] private float speedUpValue;

    [SerializeField] private int spawnRateUp;
    [SerializeField] private float spawnRateValue;

    [SerializeField] private float slowDownValue;

    [SerializeField] public float timeToDifficultUp;
    [SerializeField] private float difficultMultiplier;
    private float baseTimeDifficultUp;
    
    [SerializeField] private BossManager bossManager;
    [SerializeField] private int bossSpawnRate;
    
    
    [SerializeField] private ProgressBar progressBar;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ballSpawner.BallsCreated += AcceptBallEvent;
        Application.targetFrameRate = 80;
        baseTimeDifficultUp = timeToDifficultUp;
    }

    private void OnDisable()
    {
        ballSpawner.BallsCreated -= AcceptBallEvent;
    }

    public void AcceptBallEvent(Ball ball)
    {
        ball.BallCliked += CheckGameStatus;
        ball.BallDied += CheckGameStatus;
    }

    private void CheckGameStatus(Ball ball)
    {
        if (CheckIsLost())
        {
            uiManager.ShowEndGamePanel(true);
            uiManager.ChangeScoreValues();
            ball.BallCliked -= CheckGameStatus;
            ball.BallDied -= CheckGameStatus;
            Time.timeScale = 0;
        }
    }


    public void DifficultUp()
    {
        if (progressBar.gameStageId % (bossSpawnRate * 2) == 0)
        {
            timeToDifficultUp = baseTimeDifficultUp*1.5f;
        }
        foreach (var ball in ballSpawner.allBallsList)
        {
            ball.fallSpeed += speedUpValue;
        }
        ballSpawner.spawnRate += spawnRateValue;
        timeToDifficultUp += timeToDifficultUp * difficultMultiplier;
        progressBar.ResetProgressBar(timeToDifficultUp);
        AddNewBall();
        TrySpawnBoss();
        uiManager.gameStageText.text = progressBar.gameStageId.ToString();
    }

    private void AddNewBall()
    {
        foreach (var ball in ballSpawner.allBallsList)
        {
            if (ball.ballGameStage == progressBar.gameStageId)
            {
                ballSpawner.AddNewBall(ballSpawner.allBallsList.IndexOf(ball));
                uiManager.DrawNextBall(null);
            }
            if (ball.ballGameStage == progressBar.gameStageId + 1)
            {
                uiManager.DrawNextBall(ball);
            }

        }
    }

    private void TrySpawnBoss()
    {
        if (progressBar.gameStageId % bossSpawnRate == 0)
        {
            Boss boss = bossManager.SpawnBoss();
            if (boss.GetComponent<BossChanger>())
            {
                ballSpawner.currentBallList[0].dropChance = 0;
                ballSpawner.currentBallList[1].GetComponent<SpikedBall>().dropChance = 70;
                ballSpawner.currentBallList[1].GetComponent<SpikedBall>().spikeDamage = 0;
                ballSpawner.currentBallList[1].GetComponent<SpikedBall>().ballDamage = 50;
            }
        }
    }

    public void ResetBossChanger()
    {
        ballSpawner.currentBallList[0].dropChance = 90;
        ballSpawner.currentBallList[1].GetComponent<SpikedBall>().dropChance = 8;
        ballSpawner.currentBallList[1].GetComponent<SpikedBall>().spikeDamage = 30;
        ballSpawner.currentBallList[1].GetComponent<SpikedBall>().ballDamage = 0;
    }
    

    public void SlowDownGame(Ball ball)
    {
        for (int i = 0; i < ballSpawner.currentBallList.Count; i++)
        {
            ballSpawner.currentBallList[i].fallSpeed = ball.fallSpeed - slowDownValue;
        }

        ballSpawner.spawnRate -= 0.045f;
    }


    private bool CheckIsLost()
    {
        if (uiManager.GetCurrentHealth() <= 0)
        {
            return true;
        }

        return false;
    }
}