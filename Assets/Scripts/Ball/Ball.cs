using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum DeathReason
{
    DeathZone,
    KilledByPlayer
}
public class Ball : MonoBehaviour
{
    [SerializeField] public float fallSpeed = 5;
    [SerializeField] public int ballValue = 4;
    [SerializeField] public int ballDamage = 15;
    
    public ParticleSystem deathParticles;
    [SerializeField] public int id;
    [SerializeField] public int dropChance;
    [SerializeField] public int ballHealth;

    [SerializeField] public int ballGameStage;

    public Action<Ball> BallCliked;
    public Action<Ball> BallDied;
    public Vector3 endPos;
    

    public virtual void Start()
    {
        deathParticles = GetComponentInParent<ParticleSystem>();
        
        endPos = new Vector3(transform.position.x, transform.position.y - 16, 0);
       
    }

    private void Update()       
    {
        FallDown();
    }

    public void Init(int id)
    {
        this.id = id;
    }
    public virtual void FallDown()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * fallSpeed);
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            KilledByDeathZone();
        }
        
    }

    private void OnMouseDown()
    {
        ClickedByPlayer();
    }
    
    public virtual void ClickedByPlayer()
    {
        BallCliked.Invoke(this);
        if (ballHealth <= 0)
        {
            PlayParticles();
            DestroyBall(gameObject);
        }
    }
    public virtual void KilledByDeathZone()
    {
        BallDied.Invoke(this);
        DestroyBall(gameObject);
    }
    
    public virtual void PlayParticles()
    {
        deathParticles.Play();
    }
    
    public void DestroyBall(GameObject ball)
    {
        ball.GetComponent<SpriteRenderer>().enabled = false;
        ball.GetComponent<CircleCollider2D>().enabled = false;
        Destroy(ball, 1);
    }
}



