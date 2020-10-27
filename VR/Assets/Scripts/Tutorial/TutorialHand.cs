using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using TMPro;

public class TutorialHand : MonoBehaviour
{

	public GameObject m_Pointer;
	public GameObject lb;
	public GameObject fish;
	public TextMeshPro instruction;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean hookAction;
	public SteamVR_Action_Boolean grabAction;
	public Rigidbody rb;
	private SpringJoint sj;
	private State state = State.intro;
	private bool fishWithinRange = false;
	private float groundHeight = 0f;
	private float arcDistance = 10f;
    private float scale = 1;
	private Vector3 hookVelocity;
	private Vector3 prevPos;
	private float startGrabTime;
	private float movementCount;

	private enum State
	{
		intro,
		thumbTest1,
		thumbTest2,
		foreTest,
		rodTest1,
		rodTest2,
		oarTest1,
		oarTest2,
		oarTest3,
		endTest
	}

	void Start()
	{
		instruction.text = "233";
		lb.GetComponent<LoadingBar>().resetLen();
		sj = GetComponent<SpringJoint>();
		fish.SetActive(false);
		m_Pointer.SetActive(false);
		//sj.spring = 0f;
	}

	void Update()
	{
		switch (state)
		{
			case State.intro:
				StartCoroutine(intro1());
				StartCoroutine(intro2());
				break;
			case State.thumbTest1:
				thumb1();
				break;
			case State.thumbTest2:
				thumb2();
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
			case State.oarTest1:
				StartCoroutine(oar1());
				break;
			case State.oarTest2:
				oar2();
				break;
			case State.oarTest3:
				oar3();
				break;
			case State.endTest:
				endScene();
				break;
		}

	}

	private IEnumerator intro1() {
		instruction.fontSize = 50;
		instruction.alignment = TextAlignmentOptions.TopLeft;
		instruction.text = "Welcome to Fishing Simulator!";
		yield return new WaitForSeconds(3f);
	}
	private IEnumerator intro2() {
		instruction.text = "Let's get familiar with the controllers!";
		yield return new WaitForSeconds(3f);
		state = State.thumbTest1;
	}
	private void thumb1(){
		instruction.text = "First, please use any thumb to press the touch pad.\nPress and hold the trigger to skip this tutorial.";
		if (grabAction.GetState(handType)) {
			state = State.foreTest;
		}
		lb.GetComponent<LoadingBar>().resetLen();
		if (hookAction.GetState(handType)) {
			startGrabTime = Time.time;
			state = State.thumbTest2;
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
	private void fore(){
		instruction.text = "Good! Next, please use any index finger to press the trigger.";
		if (hookAction.GetState(handType)) {
			fish.SetActive(true);
			m_Pointer.SetActive(true);
			state = State.rodTest1;
		}
	}
	private void rod1(){
		instruction.text = "Good! Now there appears a flashing sign and a fish, move it to the fish and press the trigger.";
		updatePointer();
		if (hookAction.GetState(handType) && fishWithinRange) {
			startDragCount();
			state = State.rodTest2;
		}
	}
	private void rod2(){
		instruction.text = "The fish bites the hook! Now wave your arms up and down! The bar below shows you progress.";
		Vector3 t = transform.position - prevPos;
		movementCount += t.magnitude;
		prevPos = transform.position;
	}
	private IEnumerator oar1(){
		instruction.text = "One more thing: grab your oar. Later you will use it to boat.";
		m_Pointer.SetActive(false);
		yield return new WaitForSeconds(3f);
		state = State.oarTest2;
	}
	private void oar2() {
		instruction.text = "Press and hold touch bar to grab it for 3 seconds.";
		lb.GetComponent<LoadingBar>().resetLen();
		if (grabAction.GetState(handType)) {
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			fish.transform.SetParent(this.transform);
			startGrabTime = Time.time;
			state = State.oarTest3;
		}
	}
	private void oar3() {
		instruction.text = "Press and hold touch bar to grab it for 3 seconds.";
		lb.GetComponent<LoadingBar>().updateLen(Time.time - startGrabTime, 3f);
		if (!grabAction.GetState(handType)) {
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.None;
			fish.transform.SetParent(null);
			lb.GetComponent<LoadingBar>().resetLen();
			state = State.oarTest2;
		}
		if (Time.time - startGrabTime > 3f) {
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.None;
			fish.transform.SetParent(null);
			startGrabTime = Time.time;
			state = State.endTest;
		}
	}
	private void endScene() {
		instruction.text = "Congratulations! You've been familiar with the controllers. Please be prepared to enter the fishing world!";
		lb.GetComponent<LoadingBar>().updateLen(5f - Time.time + startGrabTime, 5f);
		if (Time.time - startGrabTime > 5f) {
			SteamVR_Fade.Start(Color.white, 1.5f, true);
			SceneManager.LoadScene(1);
		}
	}
	private void updatePointer(){

		Vector3 gravity = Physics.gravity;
		Vector3 projectileVelocity = transform.forward * (arcDistance + (-transform.rotation.x * 10 > 0 ? -transform.rotation.x * 10 : 0));
		Vector3 arcPos = Vector3.zero;

		float endTime = 0;
		for (float time = 0; time < 1000; time += 0.01f){
			arcPos = transform.position + ((projectileVelocity * time) + (0.5f * time * time) * gravity) * scale;
			endTime = time;
			if (Mathf.Abs(arcPos.y - groundHeight) < 0.1f) {
				hookVelocity = (projectileVelocity + time * gravity)/7.5f;
				break;
			}
		}

		arcPos.y = 0;

		m_Pointer.transform.position = arcPos;
		
	}
	public void startDragCount(){
		prevPos = transform.position;
		movementCount = 0f;
	}
	public void endDragCountFail(){
		state = State.rodTest1;
	}
	public void endDragCountSuccess(){
		state = State.oarTest1;
	}
	public Vector3 getHookVelocity(){
		return hookVelocity;
	}
	public float getMovementCount(){
		return movementCount;
	}

	public void fishEntered(){
		fishWithinRange = true;
	}
	public void fishLeft(){
		fishWithinRange = false;
	}

}
