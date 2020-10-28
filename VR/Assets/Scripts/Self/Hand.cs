using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using TMPro;

public class Hand : MonoBehaviour
{
	public GameObject m_Pointer;
	public TextMeshPro instruction;
	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Boolean hookAction;
	public SteamVR_Action_Boolean grabAction;
	public Rigidbody rb;
	private SpringJoint sj;
    private State state = State.free;
	private bool fishWithinRange = false;
	private float groundHeight = 0;
	private float arcDistance = 10f;
    private float scale = 1;
	private Vector3 hookVelocity;
	private Vector3 prevPos;
	private float movementCount;
	private enum State
	{
		free,
		hooking,
		biting
	}
	void Start(){
		sj = GetComponent<SpringJoint>();
	}

	void Update()
	{
        switch (state)
        {
            case State.free:
				if (grabAction.GetState(handType)) {
					rb.constraints = RigidbodyConstraints.None;
					m_Pointer.SetActive(false);
					sj.spring = 4f;
				}
				else {
					rb.constraints = RigidbodyConstraints.FreezeAll;
					m_Pointer.SetActive(true);
					sj.spring = 0f;
				}
				updatePointer();
				if (hookAction.GetState(handType)) {
					m_Pointer.GetComponent<Hook>().dropHook();
					state = State.hooking;
				}
                break;
            case State.hooking:
				if (hookAction.GetState(handType) && !fishWithinRange) {
					m_Pointer.GetComponent<Hook>().dragHook();
					state = State.free;
				}
				break;
            case State.biting:
                hooking();
                break;
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
		instruction.text = "The fish bites the hook! Now wave your arms up and down! The bar below shows you progress.";
		Vector3 t = transform.position - prevPos;
		movementCount += t.magnitude;
		prevPos = transform.position;
	}
	public void startDragCount(){
		fishWithinRange = true;
		prevPos = transform.position;
		movementCount = 0f;
	}
	public void endDragCount(){
		fishWithinRange = false;
		state = State.free;
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
}
