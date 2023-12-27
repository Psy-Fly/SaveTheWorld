using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Ball
{
    [SerializeField] private int bulletDamage;
    public override void Start()
    {
        endPos = new Vector3(transform.position.x, transform.position.y - 16, 0);
    }

    public override void ClickedByPlayer()
    {
        endPos = new Vector3(transform.position.x, transform.position.y + 16, 0);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss>().ApplyDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    public override void KilledByDeathZone()
    {
        GameManager.instance.uiManager.ChangeHealth(ballDamage);
        Destroy(gameObject);
    }
}
