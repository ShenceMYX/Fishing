using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_test : MonoBehaviour
{
	private float startTime;
	private float forceSize = 10000f;
	private float torqueSize = 10f;
	private Rigidbody rb;
	public GameObject boat;
	public GameObject leftHand;
	public GameObject rightHand;
	public GameObject leftOar;
	public GameObject rightOar;
	private Rigidbody rbLeft;
	private Rigidbody rbRight;
	private float leftPrevZ;
	private float rightPrevZ; 
	private float threshold = 0.01f;
	private float currentAngle = 180;
	Quaternion target;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		rb = this.GetComponent<Rigidbody>();
		rbLeft = leftHand.GetComponent<Rigidbody>();
		rbRight = rightHand.GetComponent<Rigidbody>();
		leftPrevZ = leftHand.transform.localPosition.z;
		rightPrevZ = rightHand.transform.localPosition.z;
		target = transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		bool leftGrabbed = (leftHand.GetComponent<Hand>().GetGrab() && leftHand.transform.localPosition.y > 1.2);
		bool rightGrabbed = (rightHand.GetComponent<Hand>().GetGrab() && rightHand.transform.localPosition.y > 1.2);
		//Debug.Log(rightHand.transform.localPosition.y);
		Vector3 leftV = rbLeft.velocity;
		Vector3 rightV = rbRight.velocity;
		float lV = (leftGrabbed ? leftHand.transform.localPosition.z - leftPrevZ : 0f);
		float rV = (rightGrabbed ? rightHand.transform.localPosition.z - rightPrevZ : 0f);
		lV = (Mathf.Abs(lV) > threshold ? lV : 0);
		rV = (Mathf.Abs(rV) > threshold ? rV : 0);
		//Debug.Log(lV + " " + rV);
		if (lV > 0 && rV > 0) {
			addForce(lV + rV);
		}
		else if (lV < 0 && rV < 0) {
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
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10f);
	}

	public void addForce(float df) {
		rb.AddForce(transform.forward * df * forceSize);
	}

	public void addTorque(float df) {
		currentAngle += df * torqueSize;
		currentAngle = Mathf.Repeat(currentAngle, 360);
		target = Quaternion.Euler(0,currentAngle,0);
	}
}
