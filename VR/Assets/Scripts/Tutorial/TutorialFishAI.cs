using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

///<summary>
///鱼AI类：控制鱼运动、行为
///</summary>
[RequireComponent(typeof(TutorialFishMotor))]

public class TutorialFishAI : MonoBehaviour
{
	public GameObject hand;
	public GameObject lb;
	public TextMeshPro instruction;
    private Animator anime;
    private TutorialFishMotor motor;
    private TutorialFishInfo info;
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
        biting
    }

    private void Start()
    {    
        rb = GetComponent<Rigidbody>();
        motor = GetComponent<TutorialFishMotor>();
        info = GetComponent<TutorialFishInfo>();
        anime = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.pathfinding:
                //motor.Pathfinding();
                break;
            case State.biting:
                Bite();
                break;
        }
    }

	public void startDrag (Vector3 velocity) {
		hand.GetComponent<TutorialHand>().startDragCount();
		startBiteTime = Time.time;
        state = State.biting;
		popV = velocity;
		Debug.Log("I've been caught");
	}
	private void Bite()
    {
		anime.SetBool("eat", true);
		lb.GetComponent<LoadingBar>().updateLen(hand.GetComponent<TutorialHand>().getMovementCount(), info.getDragPower());
		if(hand.GetComponent<TutorialHand>().getMovementCount() > info.getDragPower())
        {
            anime.SetBool("success", true);
            anime.SetBool("eat", true);
			state = State.dummy;
			startBiteTime = 0;
			popOut(popV);
			hand.GetComponent<TutorialHand>().endDragCount();
        }
    }
	private void popOut(Vector3 velocity) {
		velocity = -velocity;
		GetComponent<Rigidbody>().useGravity = true;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		rb.AddForce(velocity * rb.mass * 5f, ForceMode.Impulse);
	}
	public void getCaught(GameObject rod, Vector3 v) {
		hand = rod;
		startDrag(v);
	}
}
