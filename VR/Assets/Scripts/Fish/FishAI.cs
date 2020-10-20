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
    private State state = State.pathfinding;

    public float accelerate = 2;

    private float startBiteTime;
    private float startScaredTime;

    public float scaredTime = 4f;

    public float scaredSiwimmingSpeed = 4;

    private GameObject fishInfoUI;

    private Collider boatCollider;

    enum State
    {
        //寻路状态
        pathfinding,
        //咬钩状态
        biting,
        //受惊状态
        scared
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
                Scared();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "rod":
                state = State.biting;
                break;
            case "boat":
                boatCollider = other;
                state = State.scared;
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
        }
        else
        {
            //钓鱼成功
            //显示钓到鱼的信息
            fishInfoUI = GameObject.FindGameObjectWithTag("FishInfoUI");
            fishInfoUI.SetActive(true);
            fishInfoUI.GetComponent<ShowFishInfo>().ShowFishInfoUI(info.name, info.fishWeight);
            Destroy(gameObject);
        }
    }

    private void Scared()
    {
        startScaredTime = scaredTime;
        startScaredTime -= Time.deltaTime;
        this.transform.Translate((transform.position - boatCollider.transform.position) * scaredSiwimmingSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        //超过受惊时间并且离开小船碰撞范围继续寻路
        if (other.tag == "boat" && scaredTime < 0)
        {
            state = State.pathfinding;
            startScaredTime = scaredTime;
        }
    }
}
