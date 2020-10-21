using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鱼运动类：移动（随机速度、随机位置）、旋转
///</summary>
public class FishMotor : MonoBehaviour
{
    public Transform[] wayPoint;
    private int currentIndex;
    public int randomIndex;
    private float randomMovingTime;
    private float randomWaitingTime;
    private bool moving = true;

    public float maxSpeed = 0.5f;
    public float minSpeed = 0.1f;
    private float speed;
    public float rotateSpeed = 0.1f;
    public float minWaitingTime = 3;
    public float maxWaitingTime = 10;
    public float minMovingTime = 3;
    public float maxMovingTime = 10;


    private float startMovingTime;
    public float startWaitingTime = 100;

    private void Start()
    {
        wayPoint = transform.parent.GetComponentInChildren<WayPoint>().wayPoint;
    }

    public void Pathfinding()
    {
        if (moving)
        {
            MoveToTargetPoint(wayPoint[currentIndex].position, speed, rotateSpeed);
            randomMovingTime = Random.Range(minMovingTime, maxMovingTime);
            startMovingTime += Time.deltaTime;
        }

        //movingTime结束开始等待 生成一个随机的等待时间 startWaitingTime开始计时
        if (startMovingTime > randomMovingTime)
        {
            moving = false;
            randomWaitingTime = Random.Range(minWaitingTime, maxWaitingTime);
            startWaitingTime += Time.deltaTime;
        }

        //等待时间结束 开始移动 等待计时器清零 生成随机移动时间 移动计时器重新计时
        if (startWaitingTime > randomWaitingTime)
        {
            moving = true;
            GenerateRandomSpeedAndIndex();
            startWaitingTime = 0;
            startMovingTime = 0;
        }

    }

    public int RandomSelectWayPoint()
    {
        return Random.Range(0, wayPoint.Length); 
    }

    public void GenerateRandomSpeedAndIndex()
    {
        while (randomIndex == currentIndex)
        {
            randomIndex = RandomSelectWayPoint();
        }
        currentIndex = randomIndex;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    public void MoveToTargetPoint(Vector3 targetPoint, float moveSpeed,float rotateSpeed)
    {
        RotateToTargetPoint(targetPoint, rotateSpeed);
        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    public void RotateToTargetPoint(Vector3 targetPoint, float speed)
    {
        //transform.LookAt(targetPoint);
        Quaternion dir = Quaternion.LookRotation(targetPoint - this.transform.position);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, dir, speed);
    }
}
