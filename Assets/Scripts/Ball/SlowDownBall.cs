using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownBall : Ball
{
    public override void ClickedByPlayer()
    {
        BallCliked.Invoke(this);
        if (ballHealth <= 0)
        {
            GameManager.instance.SlowDownGame(this);
            PlayParticles();
            DestroyBall(gameObject);
        }
    }
}
