using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_test : MonoBehaviour
{
	private float startTime;
	private float forceSize = 150f;
	private float torqueSize = 50f;
	private Rigidbody rb;
	public GameObject boat;
	public GameObject leftHand;
	public GameObject rightHand;
	public GameObject leftOar;
	public GameObject rightOar;
	private Rigidbody rbLeft;
	private Rigidbody rbRight;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		rb = this.GetComponent<Rigidbody>();
		rbLeft = leftHand.GetComponent<Rigidbody>();
		rbRight = rightHand.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		bool leftGrabbed = leftHand.GetComponent<Hand>().GetGrab();
		bool rightGrabbed = rightHand.GetComponent<Hand>().GetGrab();
		Vector3 leftV = leftOar.GetComponent<VDetector>().getV();
		Vector3 rightV = rightOar.GetComponent<VDetector>().getV();
		float lV = (leftGrabbed ? Vector3.Dot(leftV, this.transform.forward) : 0f);
		float rV = (rightGrabbed ? Vector3.Dot(rightV, this.transform.forward) : 0f);
		Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.yellow, 2.5f);
		addForce(lV + rV);
		addTorque(lV - rV);
	}

	public void addForce(float df) {
		rb.AddForce(transform.forward * df * forceSize);
	}

	public void addTorque(float df) {
		rb.AddTorque(transform.up * df * torqueSize);
	}
}
