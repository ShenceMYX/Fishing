using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialFish: MonoBehaviour
{
	public GameObject hand;
	public GameObject lb;
	public TextMeshPro instruction;
	private FishInfo info;
    private Rigidbody rb;
	private bool beingDragged = false;
    private float startBiteTime;
	private Vector3 popV;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		info = GetComponent<FishInfo>();
		transform.position = new Vector3(0,3,5);
    }

    // Update is called once per frame
    void Update()
    {
		if (beingDragged){
			Bite();
		}
    }

	public void startDrag (Vector3 velocity) {
		hand.GetComponent<TutorialHand>().startDragCount();
		startBiteTime = Time.time;
		beingDragged = true;
		popV = velocity;
	}
    private void Bite()
    {
		lb.GetComponent<LoadingBar>().updateLen(hand.GetComponent<TutorialHand>().getMovementCount(), info.getDragPower());
        if (Time.time - startBiteTime > info.getDragTime())
        {
			beingDragged = false;
            startBiteTime = 0;
			hand.GetComponent<TutorialHand>().endDragCountFail();
			lb.GetComponent<LoadingBar>().resetLen();
        }
        else if(hand.GetComponent<TutorialHand>().getMovementCount() > info.getDragPower())
        {
			beingDragged = false;
			startBiteTime = 0;
			popOut(popV);
			hand.GetComponent<TutorialHand>().endDragCountSuccess();
        }
    }
	private void popOut(Vector3 velocity) {
		velocity = -velocity;
		rb.AddForce(velocity * rb.mass * 5f, ForceMode.Impulse);
	}
}
