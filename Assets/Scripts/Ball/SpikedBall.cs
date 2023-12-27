using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBall : Ball
{
    [SerializeField] public int spikeDamage;
    [SerializeField] public int spikedBallValue;

    private void Awake()
    {
    }

    public override void ClickedByPlayer()
    {
        if (ballHealth <= 0)
        {
            PlayParticles();
            GameManager.instance.uiManager.ChangeHealth(spikeDamage);
            DestroyBall(gameObject);
        }
        BallCliked.Invoke(this);
    }

    public override void KilledByDeathZone()
    {
        GameManager.instance.uiManager.ChangeScore(spikedBallValue);
        BallDied.Invoke(this);
        DestroyBall(gameObject);
    }
}
