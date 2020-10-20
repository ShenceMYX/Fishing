using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
	public SpringJoint sj;
	private bool isGrabbed = true;
	private float speed = 1f;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean grabAction;

	void Update()
	{
		
		float transV = Input.GetAxis("Vertical") * speed;
		float transH = Input.GetAxis("Horizontal") * speed;
		float transJ = Input.GetAxis("Jump") * speed;

		transV *= Time.deltaTime;
		transH *= Time.deltaTime;
		transJ *= Time.deltaTime;

		transform.Translate(transH, transJ, transV);

		if (Input.GetMouseButtonDown(0)) {
			if (isGrabbed == true) {
				sj.spring = 0f;
				isGrabbed = false;
			}
			else {
				sj.spring = 7.5f;
				isGrabbed = true;
			}
		}

		if (GetGrab()) {
			Debug.Log("Grab " + handType);
		}

	}

	public bool GetGrab() {
		return grabAction.GetState(handType);
	}
}
