using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VDetector : MonoBehaviour
{
	private bool isEntered = false;
	private GameObject sea;
	private Rigidbody rb;
	public Vector3 velocity;
	void Start()
	{
		rb = this.GetComponent<Rigidbody>();
		velocity = Vector3.zero;
	}

	void Update()
	{

		if (isEntered) {
			velocity = rb.velocity;
		}
		else {
			velocity = Vector3.zero;
		}

	}

	private void OnTriggerEnter (Collider other) {
		Debug.Log("entered");
		isEntered = true;
	}

	private void OnTriggerExit (Collider other) {
		Debug.Log("exited");
		isEntered = false;
	}

	public Vector3 getV (){
		return velocity;
	}

}
