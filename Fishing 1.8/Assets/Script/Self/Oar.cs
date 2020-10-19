using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oar : MonoBehaviour
{
	public GameObject rightHand;
	private bool isHandEntered = false;
	private GameObject entered;
	private bool isGrabbed = false;
	void Start()
	{
				
	}

	void Update()
	{
		
		if (Input.GetMouseButtonDown(0)) {
			if (!isGrabbed && isHandEntered && entered == rightHand) {
				this.transform.SetParent(entered.transform);
				isGrabbed = true;
				Debug.Log("grabbed");
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
		Debug.Log("Entering");
	}

	private void OnTriggerExit (Collider other) {
		isHandEntered = false;
		Debug.Log("Exiting");
	}

}
