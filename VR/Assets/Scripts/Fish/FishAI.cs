using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///鱼AI类：控制鱼运动、行为
///</summary>
[RequireComponent(typeof(FishMotor))]

public class FishAI : MonoBehaviour
{
    private FishMotor motor;
    private FishInfo info;
    public State state = State.pathfinding;

    public float accelerate = 2;

    private float startBiteTime;
    public float startScaredTime;

    public float scaredTime = 4f;

    public float scaredSiwimmingSpeed = 4;
    public float scaredRotatingSpeed = 0.5f;

    public float moveAwaySpeed = 2;
    public float moveAwayRotateSpeed = 0.2f;
	public GameObject fishSpawn;

    private GameObject fishInfoUI;

    private Collider otherCollider;

    private bool nearBoat;

    public enum State
    {
        //寻路状态
        pathfinding,
        //咬钩状态
        biting,
        //受惊状态
        scared,
        //避开附近的鱼
        moveAway
    }

    private void Start()
    {    
        motor = GetComponent<FishMotor>();
        info = GetComponent<FishInfo>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.pathfinding:
                motor.Pathfinding();
                break;
            case State.biting:
                Bite();
                break;
            case State.scared:
                Scared(scaredSiwimmingSpeed, scaredRotatingSpeed);
                break;
            case State.moveAway:
                Scared(moveAwaySpeed, moveAwayRotateSpeed);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherCollider = other;
        switch (other.tag)
        {
            case "rod":
                state = State.biting;
                break;
            case "boat":
                nearBoat = true;
                startScaredTime = scaredTime;
                state = State.scared;
                break;
			case "island":
                nearBoat = true;
                startScaredTime = scaredTime;
                state = State.scared;
                break;
            case "fish":
                startScaredTime = scaredTime;
                state = State.moveAway;
                break;
        }
    }

    private void Bite()
    {
        startBiteTime += Time.deltaTime;
        //钓鱼失败 超过咬杆时间
        if (startBiteTime > info.bitingTime)
        {
            state = State.scared;
            startBiteTime = 0;
        }
        else if(startBiteTime < info.bitingTime && Input.GetMouseButtonDown(0))
        {
            //钓鱼成功
            //显示钓到鱼的信息
           // fishInfoUI = GameObject.FindGameObjectWithTag("FishInfoUI").transform.GetChild(0).gameObject;
           // fishInfoUI.SetActive(true);
            //fishInfoUI.GetComponent<ShowFishInfo>().ShowFishInfoUI(info.name, info.fishWeight);
			fishSpawn.GetComponent<FishSpawn>().currentCount--;
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void Scared(float moveSpeed, float rotateSpeed)
    {
        startScaredTime -= Time.deltaTime;
		Vector3 target = transform.position + transform.position - otherCollider.transform.position;
		target = new Vector3(target.x, target.y, target.z);
        motor.MoveToTargetPoint(target , moveSpeed, rotateSpeed);
        if (startScaredTime < 0 && !nearBoat) 
        {
			otherCollider = null;
            state = State.pathfinding;
            startScaredTime = scaredTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //超过受惊时间并且离开小船碰撞范围继续寻路
        if (other.tag == "boat" )
        {
            nearBoat = false;
        }
    }
}
