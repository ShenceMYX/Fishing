using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_test : MonoBehaviour
{
	private float startTime;
	private float forceSize = 100f;
	private float torqueSize = 50f;
	private Rigidbody rb;
	public GameObject boat;
	public Rigidbody leftVelocity;
	public Rigidbody rightVelocity;
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		rb = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		float leftV = Vector3.Dot(leftVelocity.velocity, this.transform.forward);
		float rightV = Vector3.Dot(rightVelocity.velocity, this.transform.forward);
		Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.yellow, 2.5f);
		addForce(leftV + rightV);
		addTorque(leftV - rightV);
	}

	public void addForce(float df) {
		rb.AddForce(transform.forward * df * forceSize);
	}

	public void addTorque(float df) {
		rb.AddTorque(transform.up * df * torqueSize);
	}
}
