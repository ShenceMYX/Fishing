using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using TMPro;

public class TutorialSelf : MonoBehaviour
{

	public GameObject lb;
	public GameObject leftHand;
	public GameObject rightHand;
	public TextMeshPro instruction;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean hookAction;
	public SteamVR_Action_Boolean grabAction;
	private State state = State.intro1;
	private float rotationTutorialTarget = 0.965f;
	private float positionTutorialTarget = 10f;
	private float forceSize = 10f;
	private float torqueSize = 25f;
	private float currentAngle = 180;
	private float startGrabTime;
	private Vector3 currentPos;
	private Vector3 targetPos;
	private float leftPrevZ;
	private float rightPrevZ; 
	private float threshold = 0.01f;
	private float timer = -1;
	private bool playCheck = false;
	Quaternion rotationTarget;

	private enum State
	{
		intro1,
		intro2,
		thumbTest1,
		thumbTest2,
		foreTest,
		rodTest1,
		rodTest2,
		rodTest3,
		oarTest1,
		oarTest2,
		oarTest3,
		endTest
	}

	void Start()
	{
		leftPrevZ = leftHand.transform.localPosition.z;
		rightPrevZ = rightHand.transform.localPosition.z;
		rotationTarget = transform.rotation;
		currentPos = transform.position;
		targetPos = transform.position;
		instruction.text = "233";
		lb.GetComponent<LoadingBar>().resetLen();
	}

	void Update()
	{
		bool leftGrabbed = (leftHand.GetComponent<TutorialHand>().GetGrab() && leftHand.transform.localPosition.y > 1.2);
		bool rightGrabbed = (rightHand.GetComponent<TutorialHand>().GetGrab() && rightHand.transform.localPosition.y > 1.2);
		float lV = (leftGrabbed ? leftHand.transform.localPosition.z - leftPrevZ : 0f);
		float rV = (rightGrabbed ? rightHand.transform.localPosition.z - rightPrevZ : 0f);
		lV = (Mathf.Abs(lV) > threshold ? lV : 0);
		rV = (Mathf.Abs(rV) > threshold ? rV : 0);
		switch (state)
		{
			case State.intro1:
				intro1();
				break;
			case State.intro2:
				intro2();
				break;
			case State.thumbTest1:
				thumb1();
				break;
			case State.thumbTest2:
				thumb2();
				break;
			case State.oarTest1:
				oar1();
				break;
			case State.oarTest2:
				addTorque(-rV);
				if (transform.rotation.y < rotationTutorialTarget) {
					playCheck = false;
					state = State.oarTest3;
					lb.GetComponent<LoadingBar>().resetLen();
				}
				else {
					oar2();
				}
				break;
			case State.oarTest3:
				if (lV > 0 && rV > 0){
					addForce(lV + rV);
				}
				if (transform.position.z < -positionTutorialTarget){
					playCheck = false;
					state = State.foreTest;
					lb.GetComponent<LoadingBar>().resetLen();
				}
				else {
					oar3();
				}
				break;
			case State.foreTest:
				fore();
				break;
			case State.rodTest1:
				rod1();
				break;
			case State.rodTest2:
				rod2();
				break;
			case State.rodTest3:
				rod3();
				break;
			case State.endTest:
				endScene();
				break;
		}
		leftPrevZ = leftHand.transform.localPosition.z;
		rightPrevZ = rightHand.transform.localPosition.z;
		transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 1f);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * 1f);
	}

	private void intro1() {
		if (timer == -1) {
			timer = Time.time;
			instruction.fontSize = 50;
			instruction.alignment = TextAlignmentOptions.TopLeft;
			instruction.text = "Welcome to Fishing Simulator!";
			FindObjectOfType<TutorialAudioManager>().play(0);
		}
		else if (Time.time - timer > 2.5f){
			timer = -1;
			state = State.intro2;
		}
	}
	private void intro2() {
		if (timer == -1) {
			timer = Time.time;
			instruction.text = "Let's get familiar with the controllers!";
			FindObjectOfType<TutorialAudioManager>().play(1);
		}
		else if (Time.time - timer > 4f){
			timer = -1;
			state = State.thumbTest1;
		}
	}
	private void thumb1(){
		if (!playCheck){
			instruction.text = "First, please use any thumb to press the touch pad.\nPress and hold the trigger to skip this tutorial.";
			FindObjectOfType<TutorialAudioManager>().play(2);
			playCheck = true;
		}
		if (grabAction.GetState(handType)) {
			state = State.oarTest1;
		}
		lb.GetComponent<LoadingBar>().resetLen();
		if (hookAction.GetState(handType)) {
			startGrabTime = Time.time;
			state = State.thumbTest2;
			playCheck = false;
		}
	}
	private void thumb2(){
		instruction.text = "First, please use any thumb to press the touch pad.\nPress and hold the trigger to skip this tutorial.";
		lb.GetComponent<LoadingBar>().updateLen(Time.time - startGrabTime, 3f);
		if (!hookAction.GetState(handType)) {
			lb.GetComponent<LoadingBar>().resetLen();
			state = State.thumbTest1;
		}
		if (Time.time - startGrabTime > 3f) {
			startGrabTime = Time.time;
			state = State.endTest;
		}
	}
	private void oar1(){
		if (timer == -1) {
			timer = Time.time;
			instruction.text = "Touch pad is used to control oars. Now, let's try to row the boat into marked area";
			FindObjectOfType<TutorialAudioManager>().play(3);
		}
		else if (Time.time - timer > 6f){
			lb.GetComponent<LoadingBar>().resetLen();
			timer = -1;
			playCheck = false;
			state = State.oarTest2;
		}
	}
	private void oar2() {
		if (!playCheck){
			instruction.text = "Please press and hold touch pad under right thumb and do rows.";
			FindObjectOfType<TutorialAudioManager>().play(4);
			playCheck = true;
		}
		lb.GetComponent<LoadingBar>().updateLen(transform.rotation.y - rotationTutorialTarget, 1f - rotationTutorialTarget);
		Debug.Log(transform.rotation.y +" "+ rotationTutorialTarget);
		rightHand.GetComponent<TutorialHand>().setState(1);
	}
	private void oar3() {
		if (!playCheck){
			instruction.text = "We've successfully rotated the boat! Now, press and hold both touch pads and do rows.";
			FindObjectOfType<TutorialAudioManager>().play(5);
			playCheck = true;
		}
		lb.GetComponent<LoadingBar>().updateLen(-transform.position.z, positionTutorialTarget);
		leftHand.GetComponent<TutorialHand>().setState(1);
	}
	private void fore(){
		if (!playCheck){
			instruction.text = "Good! Next, please use any index finger to press the trigger.";
			FindObjectOfType<TutorialAudioManager>().play(6);
			playCheck = true;
		}
		if (hookAction.GetState(handType)) {
			state = State.rodTest1;
			playCheck = false;
		}
	}
	private void rod1(){
		if (!playCheck){
			instruction.text = "Trigger is used to throw and retract hook. Now press any trigger to throw a hook.";
			FindObjectOfType<TutorialAudioManager>().play(7);
			playCheck = true;
			leftHand.GetComponent<TutorialHand>().setState(2);
			rightHand.GetComponent<TutorialHand>().setState(2);
			timer = Time.time;
		}
		if (hookAction.GetState(handType) && Time.time - timer > 1.5f) {
			state = State.rodTest2;
			playCheck = false;
		}
	}
	private void rod2() {
		if (!playCheck) {
			timer = Time.time;
			instruction.text = "If you would like to retract the hook, press the trigger again.";
			FindObjectOfType<TutorialAudioManager>().play(8);
			playCheck = true;
		}
	}
	private void rod3(){
		if (!playCheck){
			instruction.text = "The fish bites the hook! Now wave your arms up and down! The bar below shows you progress.";
			FindObjectOfType<TutorialAudioManager>().play(9);
			playCheck = true;
		}
	}
	private void endScene() {
		if (timer == -1){
			instruction.text = "Congratulations! You've been familiar with the controllers. Please be prepared to enter the fishing world!";
			FindObjectOfType<TutorialAudioManager>().play(10);
			playCheck = true;
			timer = Time.time;
		}
		lb.GetComponent<LoadingBar>().updateLen(Time.time - timer, 10f);
		if (Time.time - timer > 10f) {
			SteamVR_Fade.Start(Color.white, 1.5f, true);
			SceneManager.LoadScene(1);
		}
	}
	public void setState(int x){
		switch(x){
			case 1:state = State.rodTest3;playCheck = false;break;
			case 2:state = State.endTest;playCheck = false;timer = -1;break;
		}
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
