using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] public int bossHealth;
    [SerializeField] public float fallSpeed;
    [SerializeField] public Ball bullet;
    [SerializeField] public float bossShotSpeed;
    [SerializeField] public float bossYLocation;
    

    public Vector3 bulletSpawnPosition;
    public Vector3 endPos;

    public virtual void Start()
    {
        endPos = new Vector3(transform.position.x, transform.position.y - bossYLocation, 0);
    }

    private void Update()
    {
        FallDown();
    }

    public virtual void Attack() { }
    
    public virtual void ApplyDamage(int damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 0)
        {
            GameManager.instance.uiManager.ChangeScore(100);
            Destroy(gameObject);
        }
    }

    public virtual void FallDown()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * fallSpeed);
    }
}
