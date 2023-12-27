using System.Collections.Generic;
using UnityEngine;

namespace Bosses
{
    public class BossManager : MonoBehaviour
    {
        [SerializeField] public List<Boss> bossList;
        private int bossId = 0;

        public Boss SpawnBoss()
        {
            if (bossId < bossList.Count)
            {
                Vector3 spawnRange = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f,0.9f),1.1f,0));
            
                Boss currentBoss = Instantiate(bossList[bossId++], spawnRange, Quaternion.identity);
                return currentBoss;
            }
            else
            {
                bossId = 0;
                Vector3 spawnRange = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f,0.9f),1.1f,0));
            
                Boss currentBoss = Instantiate(bossList[bossId++], spawnRange, Quaternion.identity);
                return currentBoss;
            }
        }
    }
}