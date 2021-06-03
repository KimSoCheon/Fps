using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] enemyPool;
    public int poolSize = 10;
    public float curTime;
    public float spawnTime = 2f;
    public int spawnCnt = 1;
    public int maxSpawnCnt = 10;
    public PlayerState playerState;
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        enemyPool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; ++i)
        {
            enemyPool[i] = Instantiate(enemyPrefab) as GameObject;
            enemyPool[i].SetActive(false);
        }
    }

    void Update()
    {
        if (playerState.isDead)
            return;
        
        //if (spawnCnt > maxSpawnCnt)
            //return;

        curTime += Time.deltaTime;
        if(curTime > spawnTime)
        {

            /*
            생성
            GameObject enemy = Instantiate(enemyPrefab) as GameObject;
            float x = Random.Range(-20f, 20f);
            enemy.transform.position = new Vector3(x, 0, 20f);
            enemy.name = "ENEMY_" + spawnCnt;
            ++spawnCnt;
            */

            //오브젝트 풀링
            curTime = 0;
            for (int i = 0; i < enemyPool.Length; i++)
            {
                if (enemyPool[i].activeSelf == true)
                    continue;
                float x = Random.Range(-20f, 20f);
                enemyPool[i].transform.position = new Vector3(x, 0, 20f);
                enemyPool[i].SetActive(true);
                enemyPool[i].name = "ENEMY" + spawnCnt;
                ++spawnCnt;
                break;
            }
        }
    }
}
