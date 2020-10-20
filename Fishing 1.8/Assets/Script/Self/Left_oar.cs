using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_oar : MonoBehaviour
{
	public GameObject leftHand;
	private bool isHandEntered = false;
	private GameObject entered;
	private bool isGrabbed = false;
	private int isInWater = 0; //0 not in water, 1 forward, 2 backward
	void Start()
	{
		this.transform.SetParent(leftHand.transform, false);
	}

	void Update()
	{

		if (Input.GetAxis("LeftOar") > 0) {
			isInWater = 1;
		}
		else {
			isInWater = 0;
		}

		
		if (Input.GetMouseButtonDown(0)) {
			if (!isGrabbed && isHandEntered && entered == leftHand) {
				this.transform.SetParent(entered.transform);
				isGrabbed = true;
			}
			else if (isGrabbed) {
				this.transform.parent = null;
				isGrabbed = false;
				isHandEntered = false;
			}
		}

	}

	private void OnTriggerEnter (Collider other) {
		isHandEntered = true;
		entered = other.gameObject;
	}

	private void OnTriggerExit (Collider other) {
		isHandEntered = false;
	}

	public int getStatus(){
		return isInWater;
	}

}
