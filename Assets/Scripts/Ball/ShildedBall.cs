using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildedBall : Ball
{
    [SerializeField] private int healthPointsToHeal;
    
    public override void ClickedByPlayer()
    {
        BallCliked.Invoke(this);
        if (ballHealth <= 0)
        {
            GameManager.instance.uiManager.HealPlayer(healthPointsToHeal);
            PlayParticles();
            DestroyBall(gameObject);
        }
        
        
    }
}
