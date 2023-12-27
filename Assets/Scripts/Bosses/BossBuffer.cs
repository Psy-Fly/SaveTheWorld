using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuffer : Boss
{
    public override void Start()
    {
        endPos = new Vector3(1.9f, transform.position.y - bossYLocation, 0);
        Attack();
    }
    
    public override void Attack()
    {
        StartCoroutine(SpawnBullets());
    }
    

    private IEnumerator SpawnBullets()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
                bulletSpawnPosition = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                Ball spawnedBullet =  Instantiate(bullet, bulletSpawnPosition, Quaternion.identity);
                spawnedBullet.Init(0);
                ApplyDamage(1);
                yield return new WaitForSeconds(1 / bossShotSpeed);
        }
    }
}
