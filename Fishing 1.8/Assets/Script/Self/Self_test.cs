using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_test : MonoBehaviour
{
	private float startTime;
	private float forceSize = 100f;
	private float torqueSize = 50f;
	public GameObject boat;
	public GameObject leftOar;
	public GameObject rightOar;
	private Rigidbody rb;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		rb = boat.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		int leftStatus = leftOar.GetComponent<Left_oar>().getStatus();
		int rightStatus = rightOar.GetComponent<Right_oar>().getStatus();
		if (leftStatus > 0 && rightStatus > 0) {
			this.addForce(forceSize);
		}
		else if (leftStatus > 0) {
			this.addTorque(torqueSize);
		}
		else if (rightStatus > 0) {
			this.addTorque(-torqueSize);
		}

		Debug.Log(rb.angularVelocity.magnitude);
	}

	public void addForce(float df) {
		rb.AddForce(transform.forward * df);
	}

	public void addTorque(float df) {
		rb.AddTorque(transform.up * df);
		Debug.Log(transform.up * df);
	}
}
