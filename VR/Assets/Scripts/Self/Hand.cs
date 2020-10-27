using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Hand : MonoBehaviour
{
	public SpringJoint sj;
	private float speed = 1f;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean grabAction;

	void Start(){
		//SteamVR_Fade.Start(Color.white, 0f, true);
		//StartCoroutine(switchScene());
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

		/*
		if (GetGrab()) {
			if (isGrabbed == true) {
				sj.spring = 0f;
				isGrabbed = false;
			}
			else {
				sj.spring = 2f;
				isGrabbed = true;
			}
		}
		*/

		if (GetGrab()) {
			sj.spring = 4f;
		}
		else {
			sj.spring = 0f;
		}

	}

	public bool GetGrab() {
		return grabAction.GetState(handType);
	}

	private IEnumerator switchScene() {
		SteamVR_Fade.Start(Color.clear, 1.5f, true);
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(1);
	}
}
