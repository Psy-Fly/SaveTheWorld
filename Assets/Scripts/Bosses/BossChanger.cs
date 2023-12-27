using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChanger : Boss
{
    [SerializeField] private int timeToDie;

    public override void Start()
    {
        base.Start();
        StartCoroutine(BossChangerDying());
    }

    public IEnumerator BossChangerDying()
    {
        yield return new WaitForSeconds(timeToDie);
        GameManager.instance.ResetBossChanger();
        Destroy(gameObject);
    }
}
