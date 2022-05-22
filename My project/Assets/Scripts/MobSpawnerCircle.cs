using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawnerCircle : MonoBehaviour
{
    public GameObject theEnemy;
    public int numberOfMobs;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public GameObject[] mobs;
    private bool spawning = false;
    private Vector3 spawnPoint;
    public int radius;
    
    void Start()
    {
        spawnPoint = transform.position;
        StartCoroutine(SpawnAll());
        
    }

    public void Update()
    {
        mobs = GameObject.FindGameObjectsWithTag(theEnemy.tag);
        if ((mobs.Length < numberOfMobs) && !spawning)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
       
        spawning = true;
        yield return new WaitForSeconds(5f);
        xPos = Random.Range(-1*radius, radius);
        zPos = Random.Range(-1*radius, radius);
        Instantiate(theEnemy, new Vector3(spawnPoint.x+xPos, 15.015f, spawnPoint.z+zPos), Quaternion.identity);
        spawning = false;
       

    }

    IEnumerator SpawnAll()
    {
        while (enemyCount < numberOfMobs)
        {
            
            xPos = Random.Range(-1*radius, radius);
            zPos = Random.Range(-1*radius, radius);
            Instantiate(theEnemy, new Vector3(spawnPoint.x+xPos, 15.015f, spawnPoint.z+zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }

}
