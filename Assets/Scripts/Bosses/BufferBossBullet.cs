using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferBossBullet : Ball
{
    [SerializeField] private float timeToDestroy;
    public override void Start()
    {
        endPos = new Vector3(transform.position.x - 7, transform.position.y, 0);
        Destroy(gameObject, timeToDestroy);
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();
        if (collision.GetComponent<Ball>())
        {
            ball.ballHealth++;
            ball.ballDamage += 10;
            ball.GetComponent<Transform>().localScale += ball.GetComponent<Transform>().localScale * 0.1f;
            ball.fallSpeed++;
            Destroy(gameObject);
        }
    }
    
    public override void ClickedByPlayer() { }
    public override void KilledByDeathZone() { }
}