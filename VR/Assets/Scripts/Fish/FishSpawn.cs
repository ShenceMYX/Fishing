using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鱼生成器类：随机起始位置、随机种类
///</summary>
public class FishSpawn : MonoBehaviour
{
    public Transform[] spawnPoint;

    public GameObject[] fishType;

    public float spawnTime = 1f;
    private float startTime;

    public int maxCount = 10;
    private int currentCount;

    private void Update()
    {
        InitializeRandomFish();
    }

    private void InitializeRandomFish()
    {
        if (startTime < Time.time && currentCount < maxCount) 
        {
            int randomFishTypeIndex = Random.Range(0, fishType.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoint.Length);
            Instantiate(fishType[randomFishTypeIndex], spawnPoint[randomSpawnPointIndex].position, Quaternion.identity);
            currentCount++;
            startTime = Time.time + spawnTime;
        }
        
    }

}
