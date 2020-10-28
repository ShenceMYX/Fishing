using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

///<summary>
///鱼AI类：控制鱼运动、行为
///</summary>
[RequireComponent(typeof(FishMotor))]

public class FishAI : MonoBehaviour
{
	public GameObject hand;
	public GameObject lb;
	public TextMeshPro instruction;
    private Animator anime;
    private FishMotor motor;
    private FishInfo info;
    private Rigidbody rb;
    private State state = State.pathfinding;
    private float startBiteTime;
    public float startScaredTime;
    public float scaredTime = 4f;
    public float scaredSiwimmingSpeed = 4;
    public float scaredRotatingSpeed = 0.5f;
    public float moveAwaySpeed = 2;
    public float moveAwayRotateSpeed = 0.2f;

    private Collider otherCollider;
	private Vector3 popV;

    private bool nearBoat;

    private enum State
    {
		dummy,
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
        rb = GetComponent<Rigidbody>();
        motor = GetComponent<FishMotor>();
        info = GetComponent<FishInfo>();
        anime = GetComponentInChildren<Animator>();
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
	public void startDrag (Vector3 velocity) {
		hand.GetComponent<DumHand>().startDragCount();
		startBiteTime = Time.time;
        state = State.biting;
		popV = velocity;
		Debug.Log("I've been caught");
	}
	private void Bite()
    {
		anime.SetBool("eat", true);
		Debug.Log("biting");
		lb.GetComponent<LoadingBar>().updateLen(hand.GetComponent<DumHand>().getMovementCount(), info.getDragPower());
        if (Time.time - startBiteTime > info.getDragTime())
        {
            anime.SetBool("success", false);
            anime.SetBool("eat", false);
			state = State.scared;
            startBiteTime = 0;
			hand.GetComponent<DumHand>().endDragCount();
			lb.GetComponent<LoadingBar>().resetLen();
        }
        else if(hand.GetComponent<DumHand>().getMovementCount() > info.getDragPower())
        {
            anime.SetBool("success", true);
            anime.SetBool("eat", true);
			state = State.dummy;
			startBiteTime = 0;
			popOut(popV);
			hand.GetComponent<DumHand>().endDragCount();
        }
    }
	private void popOut(Vector3 velocity) {
		velocity = -velocity;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		rb.AddForce(velocity * rb.mass * 5f, ForceMode.Impulse);
	}

    private void Scared(float moveSpeed, float rotateSpeed)
    {
        anime.SetBool("run", true);
        startScaredTime -= Time.deltaTime;
		Vector3 target = transform.position + transform.position - otherCollider.transform.position;
		target = new Vector3(target.x, target.y, target.z);
        motor.MoveToTargetPoint(target , moveSpeed, rotateSpeed);
        if (startScaredTime < 0 && !nearBoat) 
        {
            anime.SetBool("run", false);
			otherCollider = null;
            state = State.pathfinding;
            startScaredTime = scaredTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "boat" )
        {
            nearBoat = false;
        }
    }

	public void getCaught(GameObject rod, Vector3 v) {
		hand = rod;
		startDrag(v);
	}
}
