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
    public int currentCount;

    public int randomFishTypeIndex;
    public int randomSpawnPointIndex;

    private void Update()
    {
        InitializeRandomFish();
    }

    private void InitializeRandomFish()
    {
        if (startTime < Time.time && currentCount < maxCount) 
        {
            randomFishTypeIndex = Random.Range(0, fishType.Length);
            randomSpawnPointIndex = Random.Range(0, spawnPoint.Length);
            Instantiate(fishType[randomFishTypeIndex], spawnPoint[randomSpawnPointIndex].position, Quaternion.identity);
            currentCount++;
            startTime = Time.time + spawnTime;
        }
        
    }

}
