using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using TMPro;

public class TutorialHand : MonoBehaviour
{
	public GameObject m_Pointer;
	public GameObject self;
	public GameObject fish;
	public TextMeshPro instruction;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean hookAction;
	public SteamVR_Action_Boolean grabAction;
	public Rigidbody rb;
	private SpringJoint sj;
    private State state = State.free;
	private float groundHeight = 0;
	private float arcDistance = 10f;
    private float scale = 1;
	private Vector3 hookVelocity;
	private Vector3 prevPos;
	private float movementCount = 0;
	private float timer;
	private enum State
	{
		free,
		oarTutorial,
		hookingTutorial,
		draggingTutorial
	}
	void Start(){
		fish.SetActive(false);
		sj = GetComponent<SpringJoint>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		m_Pointer.SetActive(false);
		sj.spring = 0f;
	}

	void Update()
	{
        switch (state)
        {
            case State.oarTutorial:
				if (grabAction.GetState(handType)) {
					rb.constraints = RigidbodyConstraints.None;
					sj.spring = 4f;
				}
				else {
					rb.constraints = RigidbodyConstraints.FreezeAll;
					sj.spring = 0f;
				}
                break;
            case State.hookingTutorial:
				updatePointer();
				fish.SetActive(true);
				if (hookAction.GetState(handType) && Time.time - timer > 1.5f) {
					m_Pointer.GetComponent<TutorialHook>().dropHook();
					state = State.draggingTutorial;
				}
				break;
            case State.draggingTutorial:
                hooking();
                break;
        }
		if (movementCount > 0){
			Debug.Log(movementCount);
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

		arcPos.y = groundHeight;

		m_Pointer.transform.position = arcPos;
		
	}
	private void hooking(){
		Debug.Log("Record movement");
		Vector3 t = transform.position - prevPos;
		movementCount += t.magnitude;
		prevPos = transform.position;
	}
	public void startDragCount(){
		prevPos = transform.position;
		self.GetComponent<TutorialSelf>().setState(1);
		movementCount = 0f;
	}
	public void endDragCount(){
		state = State.free;
		self.GetComponent<TutorialSelf>().setState(2);
		m_Pointer.GetComponent<Hook>().dragHook();
	}
	public Vector3 getHookVelocity(){
		return hookVelocity;
	}
	public float getMovementCount(){
		return movementCount;
	}
	public bool GetGrab(){
		return grabAction.GetState(handType);
	}
	public void setState(int x){
		switch(x){
			case 0:state = State.free;break;
			case 1:state = State.oarTutorial;break;
			case 2:state = State.hookingTutorial;m_Pointer.SetActive(true);timer = Time.time;break;
			case 3:state = State.draggingTutorial;break;
		}
	}
}
