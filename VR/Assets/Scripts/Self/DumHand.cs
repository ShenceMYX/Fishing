using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using TMPro;

public class DumHand : MonoBehaviour
{
	public GameObject m_Pointer;
	public TextMeshPro instruction;
	private SpringJoint sj;
    private State state = State.free;
	private bool fishWithinRange = false;
	private Vector3 prevPos;
	private float startGrabTime;
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
				if (Input.GetButtonDown("Fire1")) {
					m_Pointer.GetComponent<DumHook>().dropHook();
					state = State.hooking;
				}
                break;
            case State.hooking:
				if (Input.GetButtonDown("Fire1") && !fishWithinRange) {
					m_Pointer.GetComponent<DumHook>().dragHook();
					state = State.free;
				}
				break;
            case State.biting:
                hooking();
                break;
        }

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
		m_Pointer.GetComponent<DumHook>().dragHook();
	}
	public Vector3 getHookVelocity(){
		return new Vector3(3,3,3);
	}
	public float getMovementCount(){
		return 50f;
	}
}
