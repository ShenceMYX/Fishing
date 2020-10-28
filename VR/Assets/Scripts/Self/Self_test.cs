using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_test : MonoBehaviour
{
	private float startTime;
	private float forceSize = 10f;
	private float torqueSize = 25f;
	public GameObject boat;
	public GameObject leftHand;
	public GameObject rightHand;
	public GameObject leftOar;
	public GameObject rightOar;
	private Vector3 currentPos;
	private Vector3 targetPos;
	private float leftPrevZ;
	private float rightPrevZ; 
	private float threshold = 0.01f;
	private float currentAngle = 180;
	Quaternion rotationTarget;
	void Start()
	{
		leftPrevZ = leftHand.transform.localPosition.z;
		rightPrevZ = rightHand.transform.localPosition.z;
		rotationTarget = transform.rotation;
		currentPos = transform.position;
		targetPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		bool leftGrabbed = (leftHand.GetComponent<Hand>().GetGrab() && leftHand.transform.localPosition.y > 1.2);
		bool rightGrabbed = (rightHand.GetComponent<Hand>().GetGrab() && rightHand.transform.localPosition.y > 1.2);
		float lV = (leftGrabbed ? leftHand.transform.localPosition.z - leftPrevZ : 0f);
		float rV = (rightGrabbed ? rightHand.transform.localPosition.z - rightPrevZ : 0f);
		lV = (Mathf.Abs(lV) > threshold ? lV : 0);
		rV = (Mathf.Abs(rV) > threshold ? rV : 0);
		if ((lV > 0 && rV > 0)||(lV < 0 && rV < 0)) {
			addForce(lV + rV);
		}
		else if (lV > 0 || rV < 0) {
			addTorque(lV - rV);
		}
		else if (rV > 0 || lV < 0) {
			addTorque(lV - rV);
		}
		leftPrevZ = leftHand.transform.localPosition.z;
		rightPrevZ = rightHand.transform.localPosition.z;
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 1f);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * 1f);
	}

	public void addForce(float df) {
		targetPos += transform.forward * df * forceSize;
	}

	public void addTorque(float df) {
		currentAngle += df * torqueSize;
		currentAngle = Mathf.Repeat(currentAngle, 360);
		rotationTarget = Quaternion.Euler(0,currentAngle,0);
	}
}
