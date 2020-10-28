using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鱼运动类：移动（随机速度、随机位置）、旋转
///</summary>
public class FishMotor : MonoBehaviour
{
    public Transform[] wayPoint;
	private Rigidbody rb;
    private int currentIndex;
    public int randomIndex;
    private float randomMovingTime;
    private float randomWaitingTime;
    private bool moving = true;

    private float maxSpeed = 1.2f;
    private float minSpeed = 0.8f;
    private float speed;
    private float rotateSpeed = 0.1f;
    private float minWaitingTime = 3;
    private float maxWaitingTime = 10;
    private float minMovingTime = 3;
    private float maxMovingTime = 10;


    private float startMovingTime;
    public float startWaitingTime = 100;

    private void Start()
    {
        wayPoint = transform.parent.GetComponentInChildren<WayPoint>().wayPoint;
		rb = GetComponent<Rigidbody>();
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
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
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

		//this.transform.Translate(new Vector3(0,this.transform.position.y,0));

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
		Vector3 target = targetPoint - this.transform.position;
		target = new Vector3(target.x, 0, target.z);
        Quaternion dir = Quaternion.LookRotation(target);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, dir, speed);
    }
}