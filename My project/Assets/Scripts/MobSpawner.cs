using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public GameObject[] mobs;
    private bool spawning = false;
    void Start()
    {
        StartCoroutine(SpawnAll());
    }

    public void Update()
    {
        mobs = GameObject.FindGameObjectsWithTag(theEnemy.tag);
        if (mobs.Length < 10 && !spawning)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
       
        spawning = true;
        yield return new WaitForSeconds(5f);
        xPos = Random.Range(295, 328);
        zPos = Random.Range(113, 145);
        Instantiate(theEnemy, new Vector3(xPos, 15.015f, zPos), Quaternion.identity);
        spawning = false;
       

    }

    IEnumerator SpawnAll()
    {
        while (enemyCount < 10)
        {
            
            xPos = Random.Range(295, 328);
            zPos = Random.Range(113, 145);
            Instantiate(theEnemy, new Vector3(xPos, 15.015f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }


}
