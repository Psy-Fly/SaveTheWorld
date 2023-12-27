using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IBallEventHandler
{
    [SerializeField] private BallSpawner ballSpawner;
    [SerializeField] private ScoreBoard scoreBoard;
    [SerializeField] private HealthBoard healthBoard;
    [SerializeField] public EndGamePanel endGamePanel;
    [SerializeField] public TextMeshProUGUI gameStageText;

    [SerializeField] private SpriteRenderer nextBallImage;

    private ProgressBar progressBar;



    private int currentScore = 0;
    [SerializeField] private int currentHealth = 100;

    private void Start()
    {
        progressBar = GetComponentInChildren<ProgressBar>();
        DrawNextBall(ballSpawner.allBallsList[1]);
    }

    public void DrawNextBall(Ball ball)
    {
        if (ball != null)
        {
            gameStageText.text = "";
            nextBallImage.GetComponent<SpriteRenderer>().enabled = true;
            nextBallImage.sprite = ball.GetComponent<SpriteRenderer>().sprite;
            nextBallImage.transform.localScale = ball.GetComponent<SpriteRenderer>().transform.localScale*100;
            nextBallImage.color = ball.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            nextBallImage.GetComponent<SpriteRenderer>().enabled = false;
            gameStageText.text = progressBar.gameStageId.ToString();
        }
            

    }

    private void OnEnable()
    {
        ballSpawner.BallsCreated += AcceptBallEvent;
    }

    private void OnDisable()
    {
        ballSpawner.BallsCreated -= AcceptBallEvent;
    }

    public void AcceptBallEvent(Ball ball)
    {
        ball.BallCliked += CheckClickedBall;
        ball.BallDied += CheckDiedBall;
    }

    private void CheckClickedBall(Ball ball)
    {
        ball.ballHealth -= 1;
        if(ball.ballHealth <= 0)
            ChangeScore(ball.ballValue);
    }
    private void CheckDiedBall(Ball ball)
    {
        ChangeHealth(ball.ballDamage);
    }
    public void ChangeScore(int ballValue)
    {
        currentScore += ballValue;
        scoreBoard.SetScore(currentScore);
    }

    public void ChangeHealth(int damage)
    {
        currentHealth -= damage;
        healthBoard.SetHealth(currentHealth);
    }

    public void HealPlayer(int value)
    {
        currentHealth += value;
        healthBoard.SetHealth(currentHealth);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void ShowEndGamePanel(bool isShow)
    {
        if (isShow)
        {
            endGamePanel.gameObject.SetActive(true);
        }
        else
        {
            endGamePanel.gameObject.SetActive(false);
        }
    }
    public void ChangeScoreValues()
    {
        if (GetCurrentScore() > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", GetCurrentScore());
            endGamePanel.SetFinalText("You have broken the record!");
        }
        else
        {
            endGamePanel.SetFinalText("You lost :(");
        }
        endGamePanel.SetFinalScore(currentScore);
        endGamePanel.SetBestScore(PlayerPrefs.GetInt("HighScore"));
    }
    
    
}
