using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public SpringJoint sjl;
	public SpringJoint sjr;
	private bool isGrabbed = true;
	private float speed = 1f;
	void Start()
	{
		
	}

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
				sjl.spring = 0f;
				sjr.spring = 0f;
				isGrabbed = false;
			}
			else {
				sjl.spring = 7.5f;
				sjr.spring = 7.5f;
				isGrabbed = true;
			}
		}

	}
}
