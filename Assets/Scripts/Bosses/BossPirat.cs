using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPirat : Boss
{
    public override void Start()
    {
        base.Start();
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
            for (int i = 0; i < 3; i++)
            {
                bulletSpawnPosition = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                Ball spawnedBullet =  Instantiate(bullet, bulletSpawnPosition, Quaternion.identity);
                spawnedBullet.Init(0);
                yield return new WaitForSeconds(1 / bossShotSpeed);
            }

            yield return new WaitForSeconds(3);
        }
    }
}
